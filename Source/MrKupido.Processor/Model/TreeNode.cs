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

namespace MrKupido.Processor.Model
{
    public class TreeNode
    {
        //[ScriptIgnore]
        //protected static MrKupidoContext db = new MrKupidoContext();

        [ScriptIgnore]
        public TreeNode Parent { get; private set; }
        [ScriptIgnore]
        public TreeNode[] Children { get; set; }

        public char NodeType { get; private set; }
        public string UniqueName { get; protected set; }
        public string ShortName { get; protected set; }
        public string LongName { get; protected set; }
        public string ClassName { get; private set; }
        [ScriptIgnore]
        public Type ClassType { get; private set; }
        public string FullClassName { get; private set; }
        public bool IsOpen { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }
        public string IconUrl { get; set; }

        public TreeNode(Type nodeClass)
        {
            ClassName = nodeClass.Name;
            ClassType = nodeClass;
            FullClassName = nodeClass.FullName;
            Children = new TreeNode[0];

            ShortName = NameAliasAttribute.GetDefaultName(nodeClass);
            LongName = ShortName;
            UniqueName = LongName.ToUniqueString();

            if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Equipment.EquipmentBase))) NodeType = 'E';
            else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Recipe.RecipeBase))) NodeType = 'R';
            else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Ingredient.IngredientBase))) NodeType = 'I';
            else if (nodeClass.IsSubclassOf(typeof(MrKupido.Library.Nature.NatureBase))) NodeType = 'N';
            else NodeType = 'U';

            IconUrl = IconUriFragmentAttribute.GetUrl(nodeClass, "~/Content/svg/" + Char.ToLower(NodeType) + "_{0}.svg");
        }

        private static T SetChilden<T>(T root, Dictionary<string, List<Type>> children, Func<Type, T> t2tn) where T : TreeNode
        {
            if (children.ContainsKey(root.FullClassName))
            {
                root.Children = children[root.FullClassName].ConvertAll<T>(t => t2tn(t)).ToArray();
                children.Remove(root.FullClassName);

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
                nodeClasses.AddRange(currentAssembly.GetTypes().Where(t => (t.IsClass) && ((t.IsSubclassOf(rootType)) || (t == rootType)) && ((excludeType == null) || (!t.IsSubclassOf(excludeType)))).ToArray());
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

        public override string ToString()
        {
            return LongName + " (" + ClassName + ")";
        }
    }
}
