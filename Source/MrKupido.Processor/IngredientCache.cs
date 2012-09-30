using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;
using System.Reflection;

namespace MrKupido.Processor
{
    public class IngredientCache
    {
        private Indexer ri;

        public bool WasInitialized = false;
        
        public IngredientTreeNode this [string className]
        {
            get
            {
                return (IngredientTreeNode)ri.GetByClassName(className);
            }
        }

        public void Initialize()
        {
            IngredientTreeNode root = TreeNode.BuildTree(Cache.Assemblies, t => new IngredientTreeNode(t), typeof(MrKupido.Library.Ingredient.IngredientBase), typeof(MrKupido.Library.Recipe.RecipeBase));
            ri = new Indexer(root);

            WasInitialized = true;
        }
    }
}
