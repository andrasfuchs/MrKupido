using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class IngredientGroup : IngredientBase, IIngredient
    {
        private IIngredient[] ingredients;

        public int Count
        {
            get
            {
                return ingredients.Length;
            }
        }

        public IngredientGroup(IIngredient[] ingredients) : base(0.0f, MeasurementUnit.gramm)
        {
            Category = ShoppingListCategory.Mixed;
            this.ingredients = ingredients;
        }
    }
}
