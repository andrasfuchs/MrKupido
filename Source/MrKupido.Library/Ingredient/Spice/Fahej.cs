using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "cinnamon")]
    [NameAlias("hun", "fahéj")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        CaloriesPer100Gramms = 247.0f,
        CarbohydratesPer100Gramms = 229.0f,
        FatPer100Gramms = 10.4f,
        ProteinPer100Gramms = 7.3f,
        GlichemicalIndex = 10,
        InflammationFactor = -55
    )]


    [YieldOf(typeof(CinnamomumVerum))]
    public class Fahej : SingleIngredient
    {
        public Fahej(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}