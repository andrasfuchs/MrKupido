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

namespace MrKupido.Processor.Model
{
    public class TreeNode
    {
        protected static MrKupidoContext db = new MrKupidoContext();

        public TreeNode Parent { get; private set; }
        public TreeNode[] Children { get; set; }

        public string Name { get; protected set; }
        public string ClassName { get; private set; }
        public Type ClassType { get; private set; }
        public string FullClassName { get; private set; }
        public bool IsOpen { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }

        public TreeNode(Type nodeClass)
        {
            ClassName = nodeClass.Name;
            ClassType = nodeClass;
            FullClassName = nodeClass.FullName;
            Children = new TreeNode[0];

            Name = NameAliasAttribute.GetDefaultName(nodeClass);
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

        public static T BuildTree<T>(Assembly[] assemblies, Func<Type, T> t2tn, Type rootType) where T : TreeNode
        {
            T root = null;

            Dictionary<string, List<Type>> classBases = new Dictionary<string, List<Type>>();
            List<Type> nodeClasses = new List<Type>();

            foreach (Assembly currentAssembly in assemblies)
            {
                nodeClasses.AddRange(currentAssembly.GetTypes().Where(t => (t.IsClass) && ((t.IsSubclassOf(rootType)) || (t == rootType))).ToArray());
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

        public override string ToString()
        {
            return Name + " (" + ClassName + ")";
        }
    }
}
