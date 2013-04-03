using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "onion")]
    [NameAlias("hun", "hagyma", Priority = 1)]
    [NameAlias("hun", "vöröshagyma")]
    [NameAlias("hun", "hajma", Priority = 200)]
    [NameAlias("hun", "vereshagyma", Priority = 201)]
    [NameAlias("hun", "zsidószalonna", Priority = 202)]
    [NameAlias("hun", "mózespecsenye", Priority = 203)]

    [IngredientConsts(
		Category = ShoppingListCategory.Vegetable,
		GrammsPerPiece = 35,
		KCaloriesPer100Gramms = 245.0f,
		CarbohydratesPer100Gramms = 0.3f,
		FatPer100Gramms = 170.0f,
		ProteinPer100Gramms = 74.4f,
		GlichemicalIndex = 0,
		InflammationFactor = -52
		)]

    [RootOf(typeof(AlliumCepaL))]
    public class Hagyma : SingleIngredient
    {
        public Hagyma(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}