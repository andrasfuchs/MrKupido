using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "egg albumin")]
    [NameAlias("hun", "tojásfehérje")]

    [IngredientConsts(GrammsPerPiece = 36, GrammsPerLiter = 1200)]

    public class TojasFeherje : Tojas
    {
        public TojasFeherje(float amount, MeasurementUnit unit = MeasurementUnit.piece)
            : base(amount, unit)
        {
        }
    }
}
