﻿using System;
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
        [NameAlias("hun", "feldarabol", Priority = 200)]

        [NameAlias("hun", "darabold fel a(z) {0T} {1} grammos darabokra")]
        public IngredientGroup Feldarabolni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase))  || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Feldarabolni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                if (i is IngredientGroup)
                {
                    result.Add(((IngredientGroup)i).Clone(totalWeight / count, MeasurementUnit.gramm));
                }
                else
                {
                    result.Add(i.GetType().DefaultConstructor(totalWeight / count, MeasurementUnit.gramm) as IngredientBase);
                }
            }

            return new IngredientGroup(result.ToArray());
        }

        [NameAlias("hun", "felkarikáz", Priority = 200)]

        [NameAlias("hun", "karikázd fel a(z) {0T} {1} grammos darabokra")]
        public IngredientGroup Felkarikazni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase)) || ((i.Unit != MeasurementUnit.piece) && (i.Unit != MeasurementUnit.gramm))) throw new InvalidActionForIngredientException("Felkarikazni", i.Name, i.Unit);

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
