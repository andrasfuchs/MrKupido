using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading;
using System.Diagnostics;
using MrKupido.Library.Attributes;

namespace MrKupido.Processor.Model
{
    public class TreeNode
    {
        public TreeNode Parent { get; set; }
        public TreeNode[] Children { get; set; }

        public string Name { get; set; }
        public string ClassName { get; set; }
        public string FullClassName { get; set; }
        public bool IsOpen { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }

        public TreeNode(Type nodeClass)
        {
            ClassName = nodeClass.Name;
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

        public static T BuildTree<T>(string treeNamespace, Func<Type, T> t2tn, string rootClassFullname = "System.Object") where T : TreeNode
        {
            T root = null;

            Dictionary<string, List<Type>> classBases = new Dictionary<string, List<Type>>();
            List<Type> nodeClasses = new List<Type>();

            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                nodeClasses.AddRange(currentassembly.GetTypes().Where(t => (t.IsClass) && (t.Namespace == treeNamespace)).ToArray());
            }

            foreach (Type nodeClass in nodeClasses)
            {
                if (nodeClass.BaseType.FullName == rootClassFullname)
                {
                    if (root == null)
                    {
                        root = t2tn(nodeClass);
                    }
                    else
                    {
                        throw new ProcessorException("There must be only one root in '" + treeNamespace + "' namespace. Neighter '" + root.ClassName + "' nor '" + nodeClass.Name + "' have base classes defined.");
                    }
                }

                if (!classBases.ContainsKey(nodeClass.BaseType.FullName)) classBases.Add(nodeClass.BaseType.FullName, new List<Type>());

                classBases[nodeClass.BaseType.FullName].Add(nodeClass);
            }

            if (root == null) throw new ProcessorException("There must be exactly one root in '" + treeNamespace + "' namespace which has the base class of '" + rootClassFullname + "'.");
            root = TreeNode.SetChilden(root, classBases, t2tn);

            classBases.Remove(rootClassFullname);

            if (classBases.Count() > 0) throw new ProcessorException("All classes in the '" + treeNamespace + "' namespace must connect to the root '" + root.ClassName + "'. There are classes like '" + classBases.First().Value[0].Name + "' which are not connected to the main tree.");

            return root;
        }

        public override string ToString()
        {
            return Name + " (" + ClassName + ")";
        }
    }
}
