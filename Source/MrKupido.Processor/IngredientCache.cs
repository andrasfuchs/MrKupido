using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;
using System.Reflection;

namespace MrKupido.Processor
{
    public class IngredientCache : BaseCache
    {        
        public new IngredientTreeNode this [string name]
        {
            get
            {
                return (IngredientTreeNode)base[name];
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
