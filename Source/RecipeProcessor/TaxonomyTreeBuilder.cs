using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class TaxonomyTreeBuilder : ITreeBuilder
    {
        public TreeNode Build(string natureNamespace = "MrKupido.Library.Nature")
        {
            NatureTreeNode root = null;

            Dictionary<string, List<Type>> classBases = new Dictionary<string, List<Type>>();
            List<Type> natureClasses = new List<Type>();

            foreach (Assembly currentassembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                natureClasses.AddRange(currentassembly.GetTypes().Where(t => (t.IsClass) && (t.Namespace == natureNamespace)).ToArray());
            }

            foreach (Type natureClass in natureClasses)
            {
                if (natureClass.BaseType.FullName == "System.Object")
                {
                    if (root == null)
                    {
                        root = new NatureTreeNode(natureClass);
                    }
                    else
                    {
                        throw new Exception("There must be only one root in '" + natureNamespace + "' namespace. Neighter '" + root.ClassName + "' or '" + natureClass.Name + "' have base classes defined.");
                    }
                }

                if (!classBases.ContainsKey(natureClass.BaseType.Name)) classBases.Add(natureClass.BaseType.Name, new List<Type>());

                classBases[natureClass.BaseType.Name].Add(natureClass);
            }

            root = TreeNode.SetChilden(root, classBases, t => new NatureTreeNode(t));

            classBases.Remove("Object");

            if (classBases.Count() > 0) throw new Exception("All classes in the '" + natureNamespace + "' namespace must connect to the root '" + root.ClassName + "'. There are classes like '" + classBases.First().Value[0].Name + "' which are not connected to the main tree.");

            return root;
        }
    }
}
