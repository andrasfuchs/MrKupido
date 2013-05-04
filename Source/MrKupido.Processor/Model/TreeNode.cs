using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using MrKupido.Library.Attributes;
using MrKupido.DataAccess;
using MrKupido.Library;
using System.Web.Script.Serialization;
using MrKupido.Utils;

namespace MrKupido.Processor.Model
{
    public class TreeNode : ITreeNode
    {
        //[ScriptIgnore]
        //protected static MrKupidoContext db = new MrKupidoContext();

        [ScriptIgnore]
        public TreeNode Parent { get; protected set; }
        [ScriptIgnore]
        public TreeNode[] Children { get; set; }
		[ScriptIgnore]
		public DateTime CreatedAt { get; private set; }

        public char NodeType { get; private set; }
        public string LanguageISO { get; private set; }
		public string UniqueName { get; private set; }
		public string UniqueNameEng { get; private set; }
        public string ShortName { get; protected set; }
        public string LongName { get; protected set; }
        public string ClassName { get; private set; }
        [ScriptIgnore]
        public Type ClassType { get; private set; }
        public string ClassFullName { get; private set; }
        public bool IsOpen { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }
        public string[] IconUrls { get; private set; }

        private string iconUrl;
        public string IconUrl
        {
            get
            {
                if (this.iconUrl == null)
                {
                    this.iconUrl = PathUtils.GetActualUrl(this.IconUrls);
                }

                return this.iconUrl;
            }

            private set
            {
                this.iconUrl = value;
            }
        }

        public TreeNode(Type nodeClass, string languageISO)
        {
			CreatedAt = DateTime.Now;

            LanguageISO = languageISO;
            ClassName = nodeClass.Name;
            ClassType = nodeClass;
            ClassFullName = nodeClass.FullName;
            Children = new TreeNode[0];

            string name = NameAliasAttribute.GetName(languageISO, nodeClass);
            
            if (String.IsNullOrEmpty(name))
            {
                throw new MrKupidoException("Class '{0}' must have a name defined.", nodeClass.FullName);
            }

            ShortName = name;
            int bracketStart = name.IndexOf('[');
            ShortName = bracketStart == -1 ? ShortName : ShortName.Substring(0, bracketStart);
            bracketStart = ShortName.IndexOf('{');
            ShortName = bracketStart == -1 ? ShortName : ShortName.Substring(0, bracketStart);

            ShortName = ShortName.TrimEnd();
            LongName = name;
            UniqueName = LongName.ToUniqueString();
			UniqueNameEng = NameAliasAttribute.GetName("eng", nodeClass).ToUniqueString();

            if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Equipment.EquipmentBase)) || (nodeClass == typeof(MrKupido.Library.Equipment.EquipmentBase))) NodeType = 'E';
            else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Recipe.RecipeBase)) || (nodeClass == typeof(MrKupido.Library.Recipe.RecipeBase))) NodeType = 'R';
            else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Ingredient.IngredientBase)) || (nodeClass == typeof(MrKupido.Library.Ingredient.IngredientBase))) NodeType = 'I';
            else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Nature.NatureBase)) || (nodeClass == typeof(MrKupido.Library.Nature.NatureBase))) NodeType = 'N';
			else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Tag.TagBase)) || (nodeClass == typeof(MrKupido.Library.Tag.TagBase))) NodeType = 'T';
            else NodeType = 'U';

            IconUrls = IconUriFragmentAttribute.GetUrls(nodeClass, "~/Content/svg/" + Char.ToLower(NodeType) + "_{0}.svg");
        }

        private static T SetChilden<T>(T root, Dictionary<string, List<Type>> children, Func<Type, T> t2tn) where T : TreeNode
        {
            if (children.ContainsKey(root.ClassFullName))
            {
                List<TreeNode> tnChildren = new List<TreeNode>();

                foreach (Type t in children[root.ClassFullName])
                {
                    try
                    {
                        TreeNode child = t2tn(t);
                        tnChildren.Add(child);
                    }
                    catch
                    {
                        Trace.TraceError("Could not add the child node '{1}' to the parent node '{0}'.", root.UniqueName, t.FullName);
                    }
                }

                root.Children = tnChildren.ToArray();
                children.Remove(root.ClassFullName);

                foreach (T node in root.Children)
                {
                    node.Parent = root;
                    SetChilden(node, children, t2tn);
                }
            }

            return root;
        }

        public static T BuildTree<T>(Assembly[] assemblies, Func<Type, T> t2tn, Type rootType, Type excludeType = null) where T : TreeNode
        {
            T root = null;

            Dictionary<string, List<Type>> classBases = new Dictionary<string, List<Type>>();
            List<Type> nodeClasses = new List<Type>();

            foreach (Assembly currentAssembly in assemblies)
            {
                nodeClasses.AddRange(currentAssembly.GetTypes().Where(t => (t.IsClass) && ((t.IsSubclassOf(rootType)) || (t == rootType)) && ((excludeType == null) || ((!t.IsSubclassOf(excludeType)) && (t != excludeType)))).ToArray());
            }

            foreach (Type nodeClass in nodeClasses)
            {
                if (nodeClass == rootType)
                {
                        root = t2tn(nodeClass);
                }
                else
                {
                    if (!classBases.ContainsKey(nodeClass.BaseType.FullName)) classBases.Add(nodeClass.BaseType.FullName, new List<Type>());

                    classBases[nodeClass.BaseType.FullName].Add(nodeClass);
                }
            }

            if (root == null) throw new ProcessorException("There must be one '{0}' root item in the tree.'", rootType.FullName);
            root = TreeNode.SetChilden(root, classBases, t2tn);

            return root;
        }

        public static int GetDescendantCount(TreeNode tn)
        {
            int result = 0;

            foreach (TreeNode child in tn.Children)
            {
                result += GetDescendantCount(child);
            }
            result += tn.Children.Count();

            return result;
        }

		protected void AddOrSet(ref float? f1, float? f2, ref float completion, float completionStep)
		{
			if (f2.HasValue)
			{
				if (f1.HasValue)
				{
					f1 = f1 + f2;
				}
				else
				{
					f1 = f2;
				}
				completion += completionStep;
			}
		}

        public override string ToString()
        {
            return LongName + " (" + ClassName + ")";
        }
    }
}
