using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "marjoram")]
    [NameAlias("hun", "majoránna")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        CaloriesPer100Gramms = 271.0f,
        CarbohydratesPer100Gramms = 181.0f,
        FatPer100Gramms = 58.9f,
        ProteinPer100Gramms = 30.9f,
        GlichemicalIndex = 8,
        InflammationFactor = 317
    )]

    [LeafOf(typeof(OriganumMajorana))]
    public class Majoranna : SingleIngredient
    {
        public Majoranna(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit, IngredientState.Orolt)
        {
        }
    }
}