using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ingredient group")]
    [NameAlias("hun", "hozzávalók csoportja")]

    public class IngredientGroup : IngredientBase, IIngredient, IEnumerable<IIngredient>
    {
        private int id;
        public int Id
        {
            set
            {
                if (!IconUrl.Contains("{0}")) throw new MrKupidoException("The ID of an ingredientgroup should be set only once.");

                id = value;
                IconUrl = IconUrl.Replace("{0}", id.ToString("00"));
            }

            get
            {
                return id;
            }
        }

        public string IconUrl { get; set; }

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

        private string nameOverride;
        public new string Name
        {
            set
            {
                nameOverride = value;
            }

            get
            {
                if (Ingredients.Length == 0) return base.Name;
                else return (nameOverride == null ? Ingredients[0].Name : nameOverride);
            }
        }

        public IngredientGroup(params IIngredient[] ingredients) : base(0.0f, MeasurementUnit.none)
        {
            this.Category = 0;

            AddIngredients(ingredients);

            if (this.Unit != MeasurementUnit.none)
            {
                float amount = 0.0f;
                foreach (IIngredient i in Ingredients)
                {
                    amount += i.GetAmount(this.Unit);
                }
                SetAmount(amount, this.Unit);
            }

            IconUrl = "~/Content/svg/inggroup_{0}.svg";
        }

        public IngredientGroup Clone(float amount, MeasurementUnit unit)
        {
            List<IngredientBase> ibs = new List<IngredientBase>();

            float ratio = amount / GetAmount(unit);

            foreach (IIngredient i in this.Ingredients)
            {
                IngredientBase ib = (IngredientBase)i.GetType().DefaultConstructor(ratio * i.GetAmount(unit), unit);
                ib.ChangeUnitTo(i.Unit);

                ibs.Add(ib);
            }

            IngredientGroup ig = new IngredientGroup(ibs.ToArray());
            return ig;
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

                this.Category |= ingredient.Category;
            }
        }

        public override string ToString()
        {
            if (nameOverride != null) return nameOverride;

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
