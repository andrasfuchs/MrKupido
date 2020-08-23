using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pepper")]
    [NameAlias("hun", "pirospaprika", Priority = 1)]
    [NameAlias("hun", "piros fűszerpaprika", Priority = 2)]
    [NameAlias("hun", "fűszerpaprika")]

    [IngredientConsts(
        Category = ShoppingListCategory.Spice,
        CaloriesPer100Gramms = 18.0f,
        CarbohydratesPer100Gramms = 13.5f,
        FatPer100Gramms = 2.5f,
        ProteinPer100Gramms = 2.0f,
        GlichemicalIndex = 1,
        InflammationFactor = 13
    )]


    [GristOf(typeof(CapsicumAnnuumL))]
    public class Fuszerpaprika : SingleIngredient
    {
        public Fuszerpaprika(float amount, MeasurementUnit unit = MeasurementUnit.csipet)
            : base(amount, unit)
        {
        }
    }
}
