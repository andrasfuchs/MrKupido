using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class TreeNode
    {
        public TreeNode Parent { get; set; }
        public TreeNode[] Children { get; set; }

        public string Name { get; set; }
        public string ClassName { get; set; }
        public bool IsOpen { get; set; }
        public bool IsSelected { get; set; }
        public bool IsDisabled { get; set; }

        private int namePriority = Int32.MaxValue;

        public TreeNode(Type natureClass)
        {
            ClassName = natureClass.Name;
            Children = new TreeNode[0];

            foreach (object attr in natureClass.GetCustomAttributes(false))
            {
                if (attr.GetType().FullName == "MrKupido.Library.Attributes.NameAliasAttribute")
                {
                    MrKupido.Library.Attributes.NameAliasAttribute name = (MrKupido.Library.Attributes.NameAliasAttribute)attr;

                    if (name.CultureName == "hun")
                    {
                        if (name.Priority == namePriority)
                        {
                            throw new Exception("Class '" + natureClass.Name + "' has more then one name alias with the same priority.");
                        }

                        if (name.Priority < namePriority)
                        {
                            Name = name.Name;
                            namePriority = name.Priority;
                        }
                    }
                }
            }
        }

        public static T SetChilden<T>(T root, Dictionary<string, List<Type>> children, Func<Type, T> t2tn) where T : TreeNode
        {
            if (children.ContainsKey(root.ClassName))
            {
                root.Children = children[root.ClassName].ConvertAll<T>(t => t2tn(t)).ToArray();
                children.Remove(root.ClassName);

                foreach (T node in root.Children)
                {
                    node.Parent = root;
                    SetChilden(node, children, t2tn);
                }
            }

            return root;
        }

        public override string ToString()
        {
            return Name + " (" + ClassName + ")";
        }
    }
}
