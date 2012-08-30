using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "*hozzávalók csoportja")]
    [NameAlias("eng", "*ingredient group")]

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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(base.ToString());

            if (ingredients.Length > 0)
            {
                sb.Append(": [");

                for (int i = 0; i < ingredients.Length; i++)
                {
                    sb.Append(ingredients[i].ToString());

                    if (i < ingredients.Length-1) sb.Append(", ");
                }

                sb.Append("]");
            }

            return sb.ToString();
        }
    }
}
