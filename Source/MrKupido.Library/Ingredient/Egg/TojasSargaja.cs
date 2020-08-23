using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "egg yolk")]
    [NameAlias("hun", "tojássárgája")]

    [IngredientConsts(
        Category = ShoppingListCategory.Other,
        GrammsPerPiece = 17,
        CaloriesPer100Gramms = 317.0f,
        CarbohydratesPer100Gramms = 14.7f,
        FatPer100Gramms = 239.0f,
        ProteinPer100Gramms = 63.4f,
        GlichemicalIndex = 2,
        InflammationFactor = -322
    )]

    public class TojasSargaja : Tojas
    {
        public TojasSargaja(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
