using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class Indexer
    {
        private Dictionary<string, TreeNode> classNameIndex = new Dictionary<string, TreeNode>();

        public Indexer(TreeNode root)
        {
            classNameIndex.Clear();

            AddToIndex(root);
        }

        private void AddToIndex(TreeNode node)
        {
            classNameIndex.Add(node.ClassName, node as TreeNode);

            foreach (TreeNode tn in node.Children)
            {
                AddToIndex(tn);
            }
        }

        public TreeNode GetByClassName(string className)
        {
            TreeNode tn = null;
            classNameIndex.TryGetValue(className, out tn);

            if (tn == null) throw new MrKupidoException("The class '{0}' was not found in the tree.", className);

            return tn;
        }
    }
}
