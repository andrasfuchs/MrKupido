using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "yeast")]
    [NameAlias("hun", "élesztő", Priority = 1)]
    [NameAlias("hun", "élesztőpor", Priority = 2)]
    [NameAlias("hun", "szárított élesztő")]

    [IngredientConsts(
        Category = ShoppingListCategory.Other,
        GrammsPerLiter = 1000,
        CaloriesPer100Gramms = 295.0f,
        CarbohydratesPer100Gramms = 141.0f,
        FatPer100Gramms = 38.6f,
        ProteinPer100Gramms = 115.0f,
        GlichemicalIndex = 8,
        InflammationFactor = 450
    )]

    [GranulesOf(typeof(SaccharomycesCerevisiae))]
    public class Eleszto : SingleIngredient
    {
        public Eleszto(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
        }
    }
}
