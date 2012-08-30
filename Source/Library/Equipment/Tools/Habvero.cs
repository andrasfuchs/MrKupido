﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "habverő")]

    public class Habvero : Tool
    {
        public IngredientGroup Felverni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.liter)) throw new InvalidActionForIngredientException("Felverni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();


            ((IngredientBase)i).ChangeUnitTo(MeasurementUnit.gramm);

            result.Add(i);

            return new IngredientGroup(result.ToArray());
        }
    }
}
