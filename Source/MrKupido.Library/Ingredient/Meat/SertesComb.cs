using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pork leg")]
    [NameAlias("hun", "sertéscomb")]

    [IngredientConsts(
        Category = ShoppingListCategory.Meat,
        CaloriesPer100Gramms = 245.0f,
        CarbohydratesPer100Gramms = 0.3f,
        FatPer100Gramms = 170.0f,
        ProteinPer100Gramms = 74.4f,
        GlichemicalIndex = 0,
        InflammationFactor = -52
    )]

    [PartOf(typeof(SusScrofaDomestica))]
    public class SertesComb : SingleIngredient
    {
        public SertesComb(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
        }
    }
}