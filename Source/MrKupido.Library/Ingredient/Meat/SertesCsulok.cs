using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pork knuckle")]
    [NameAlias("hun", "sertéscsülök")]

    [IngredientConsts(Category = ShoppingListCategory.Meat)]

    [PartOf(typeof(SusScrofaDomestica))]
    public class SertesCsulok : SingleIngredient
    {
        public SertesCsulok(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}