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

        public IngredientTreeNode[] All
        {
            get
            {
                return Indexer.All.Cast<IngredientTreeNode>().ToArray();
            }
        }


        public void Initialize(string languageISO)
        {
            if (Indexer != null) return;

            this.language = languageISO;

            IngredientTreeNode root = TreeNode.BuildTree(Cache.Assemblies, t => new IngredientTreeNode(t, languageISO), typeof(MrKupido.Library.Ingredient.IngredientBase), typeof(MrKupido.Library.Recipe.RecipeBase));
            Indexer = new Indexer(root, languageISO);

            WasInitialized = true;
        }
    }
}
