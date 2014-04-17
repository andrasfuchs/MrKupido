using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
	[NameAlias("eng", "flour")]
	[NameAlias("hun", "liszt", Priority = 1)]
	[NameAlias("hun", "búzaliszt")]

	[IngredientConsts(
		Category = ShoppingListCategory.Other,
		CaloriesPer100Gramms = 366.0f,
		CarbohydratesPer100Gramms = 314.0f,
		FatPer100Gramms = 12.4f,
		ProteinPer100Gramms = 39.3f,
		GlichemicalIndex = 53,
		InflammationFactor = -387
	)]


	[GristOf(typeof(TriticumAestivum))]
	public class Liszt : SingleIngredient
	{
		public Liszt(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
			: base(amount, unit)
		{
		}
	}
}
