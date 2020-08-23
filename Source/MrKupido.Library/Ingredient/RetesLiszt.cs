using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "pastry flour")]
    [NameAlias("hun", "rétesliszt")]

    //[GristOf(typeof(TriticumAestivum))]
    public class RetesLiszt : Liszt
    {
        public RetesLiszt(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
