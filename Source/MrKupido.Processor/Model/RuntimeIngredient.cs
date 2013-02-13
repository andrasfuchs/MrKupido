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
        public IngredientBase Ingredient { get; private set; }
        public TreeNode TreeNode { get; private set; }

        public RuntimeIngredient(IngredientBase i, TreeNode tn)
        {
            Ingredient = i;
            TreeNode = tn;
        }
    }
}
