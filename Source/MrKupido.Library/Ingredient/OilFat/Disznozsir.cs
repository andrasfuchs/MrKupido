using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "lard", Priority = 1)]
    [NameAlias("eng", "pig fat")]
    [NameAlias("hun", "dizsnózsír", Priority = 1)]
    [NameAlias("hun", "sertészsír")]

    [IngredientConsts(GrammsPerLiter = 1000)]

    //[OilOf(typeof(SusScrofaDomestica))]
    public class Disznozsir : AllatiZsiradek
    {
        public Disznozsir(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }

}
