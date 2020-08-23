using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "olive oil")]
    [NameAlias("hun", "olívaolaj")]

    [IngredientConsts(
        Category = ShoppingListCategory.Other,
        CaloriesPer100Gramms = 884.0f,
        CarbohydratesPer100Gramms = 0.0f,
        FatPer100Gramms = 884.0f,
        ProteinPer100Gramms = 0.0f,
        GlichemicalIndex = 0,
        InflammationFactor = 526
    )]


    [OilOf(typeof(OleaEuropaea))]
    public class OlivaOlaj : SingleIngredient
    {
        public OlivaOlaj(float amount, MeasurementUnit unit = MeasurementUnit.deciliter)
            : base(amount, unit)
        {
        }
    }
}