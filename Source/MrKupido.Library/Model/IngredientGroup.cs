using MrKupido.Library.Attributes;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ingredient group")]
    [NameAlias("hun", "hozzávalók csoportja")]

    public class IngredientGroup : IngredientBase, IIngredientGroup, IEnumerable<SingleIngredient>
    {
        public int Id { set; get; }

        private List<SingleIngredient> ingredients = new List<SingleIngredient>();

        public ISingleIngredient[] Ingredients
        {
            get
            {
                return ingredients.ToArray();
            }
        }

        public int IngredientCount
        {
            get
            {
                return ingredients.Count();
            }
        }

        private string nameOverride;

        public IngredientGroup(params IIngredient[] ingredients) : base(0.0f, MeasurementUnit.none)
        {
            AddIngredients(ingredients);
        }

        public void AddIngredients(params IIngredient[] ingredients)
        {
            bool changed = false;

            foreach (IIngredient ingredient in ingredients)
            {
                if (ingredient == null) continue;

                changed = true;

                if (ingredient is IngredientGroup)
                {
                    AddIngredients(((IngredientGroup)ingredient).Ingredients);

                    if (ingredient.PieceCount > this.PieceCount)
                    {
                        this.PieceCount = ingredient.PieceCount;
                    }

                    continue;
                }

                if (ingredient is SingleIngredient)
                {
                    this.ingredients.Add((SingleIngredient)ingredient);
                    if (this.Unit == MeasurementUnit.none)
                    {
                        if (ingredient.IsSolid)
                        {
                            this.Unit = MeasurementUnit.gramm;
                        }
                        else if (ingredient.IsFluid)
                        {
                            this.Unit = MeasurementUnit.liter;
                        }
                        else
                        {
                            this.Unit = ingredient.Unit;
                        }
                    }
                }
            }

            if (changed)
            {
                Quantity.Clear();

                if (this.Unit != MeasurementUnit.none)
                {
                    float amount = 0.0f;
                    foreach (IIngredient i in Ingredients)
                    {
                        amount += i.GetAmount(this.Unit);
                    }
                    SetAmount(amount, this.Unit);
                }
            }
        }

        public ISingleIngredient RemoveIngredient(ISingleIngredient i)
        {
            ISingleIngredient result = ingredients.FirstOrDefault(ing => ing.Name == i.Name);

            if (result != null)
            {
                ingredients.Remove((SingleIngredient)result);
            }

            return result;
        }

        public void SetName(string value)
        {
            nameOverride = value;
        }

        public string GetName(string languageISO)
        {
            if (Ingredients.Length == 0) return base.GetName(languageISO);
            else return (nameOverride == null ? Ingredients[0].GetName(languageISO) : nameOverride);
        }

        public override string ToString(string languageISO)
        {
            if (nameOverride != null) return nameOverride;

            StringBuilder sb = new StringBuilder(this.GetName(languageISO));

            if (ingredients.Count() > 0)
            {
                sb.Append(": [");

                for (int i = 0; i < ingredients.Count(); i++)
                {
                    sb.Append(ingredients[i].ToString(languageISO));

                    if (i < ingredients.Count() - 1) sb.Append(", ");
                }

                sb.Append("]");
            }

            return sb.ToString();
        }

        #region IEnumerable<IIngredient> Members

        public IEnumerator<SingleIngredient> GetEnumerator()
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

        public override bool ChangeUnitTo(MeasurementUnit unit)
        {
            if (this.Unit == unit) return true;

            foreach (SingleIngredient si in ingredients)
            {
                si.ChangeUnitTo(unit);
            }

            this.Unit = unit;

            float amount = 0.0f;
            foreach (IIngredient i in Ingredients)
            {
                amount += i.GetAmount(this.Unit);
            }

            SetAmount(amount, this.Unit);
            return true;
        }
    }
}
