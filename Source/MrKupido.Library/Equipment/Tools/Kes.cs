using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "kés")]

    public class Kes : Tool
    {
        public IngredientGroup Feldarabolni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase))  || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Feldarabolni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                result.Add(i.GetType().DefaultConstructor(totalWeight / count, MeasurementUnit.gramm) as IngredientBase);
            }

            return new IngredientGroup(result.ToArray());
        }

        public IngredientGroup Felkarikazni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.piece)) throw new InvalidActionForIngredientException("Felkarikazni", i.Name, i.Unit);

            i.ChangeUnitTo(MeasurementUnit.gramm);

            IngredientGroup ig = Feldarabolni(i, weight);

            foreach (IIngredient ingredient in ig)
            {
                ingredient.ChangeUnitTo(MeasurementUnit.piece);
            }

            return new IngredientGroup(ig.ToArray());
        }
    }
}
