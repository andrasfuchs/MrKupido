using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class RecipeIndexer
    {
        private static Dictionary<string, RecipeTreeNode> classNameIndex = new Dictionary<string, RecipeTreeNode>();

        public RecipeIndexer(RecipeTreeNode root)
        {
            classNameIndex.Clear();

            AddToIndex(root);
        }

        private void AddToIndex(TreeNode node)
        {
            classNameIndex.Add(node.ClassName, node as RecipeTreeNode);

            foreach (TreeNode tn in node.Children)
            {
                AddToIndex(tn);
            }
        }

        public RecipeTreeNode GetByClassName(string className)
        {
            RecipeTreeNode rtn = null;
            classNameIndex.TryGetValue(className, out rtn);

            return rtn;
        }
    }
}
