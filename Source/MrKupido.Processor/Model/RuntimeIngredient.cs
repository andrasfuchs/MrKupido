using MrKupido.Library;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class RuntimeIngredient
    {
		public string RecipeUniqueName { get; private set; }
		public string RecipeName { get; set; }

        public IngredientBase Ingredient { get; private set; }
        public TreeNode TreeNode { get; private set; }

        public RuntimeIngredient(IngredientBase i, TreeNode tn, string run)
        {
            Ingredient = i;
            TreeNode = tn;
			RecipeUniqueName = run;
        }
    }
}
