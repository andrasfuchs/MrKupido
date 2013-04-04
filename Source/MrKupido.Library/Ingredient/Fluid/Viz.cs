using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Nature;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "water")]
    [NameAlias("hun", "víz")]

	[IngredientConsts(
		Category = ShoppingListCategory.Other,
		GrammsPerLiter = 1000,
		CaloriesPer100Gramms = 0.0f,
		CarbohydratesPer100Gramms = 0.0f,
		FatPer100Gramms = 0.0f,
		ProteinPer100Gramms = 0.0f,
		GlichemicalIndex = 0,
		InflammationFactor = 0
	)]


    [KindOf(typeof(Nonliving))]
    public class Viz  : SingleIngredient
    {
        public Viz(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }
    }
}
