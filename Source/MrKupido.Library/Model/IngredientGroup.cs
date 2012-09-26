using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "hozzávalók csoportja")]
    [NameAlias("eng", "ingredient group")]

    public class IngredientGroup : IngredientBase, IIngredient, IEnumerable<IIngredient>
    {
        private List<IIngredient> ingredients = new List<IIngredient>();

        public IIngredient[] Ingredients
        {
            get
            {
                return ingredients.ToArray();
            }
        }

        public int Count
        {
            get
            {
                return ingredients.Count();
            }
        }

        public new string Name
        {
            get
            {
                if (Ingredients.Length == 0) return base.Name;
                else return Ingredients[0].Name;
            }
        }

        public IngredientGroup(IIngredient[] ingredients) : base(0.0f, MeasurementUnit.none)
        {
            // TODO: decide the category on the individual ingredient categories
            Category = ShoppingListCategory.Mixed;

            AddIngredients(ingredients);
        }

        private void AddIngredients(IIngredient[] ingredients)
        {
            foreach (IIngredient ingredient in ingredients)
            {
                if (ingredient is IngredientGroup)
                {
                    AddIngredients(((IngredientGroup)ingredient).Ingredients);
                    continue;
                }

                this.ingredients.Add(ingredient);
                if (this.Unit == MeasurementUnit.none)
                {
                    this.Unit = ingredient.Unit;
                } else if (this.Unit != ingredient.Unit) 
                {
                    this.Unit = MeasurementUnit.gramm;
                }
            }
        }

        public override string ToString()
        {
            //StringBuilder sb = new StringBuilder(base.ToString());
            StringBuilder sb = new StringBuilder(Name);

            if (ingredients.Count() > 0)
            {
                sb.Append(": [");

                for (int i = 0; i < ingredients.Count(); i++)
                {
                    sb.Append(ingredients[i].ToString());

                    if (i < ingredients.Count()-1) sb.Append(", ");
                }

                sb.Append("]");
            }

            return sb.ToString();
        }

        #region IEnumerable<IIngredient> Members

        public IEnumerator<IIngredient> GetEnumerator()
        {
            return ingredients.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ingredients.GetEnumerator();
        }

        #endregion
    }
}
