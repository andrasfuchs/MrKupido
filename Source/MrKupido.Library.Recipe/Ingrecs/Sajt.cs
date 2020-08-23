using MrKupido.Library.Attributes;
using System;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "sajt")]
    [NameAlias("eng", "cheese")]

    [IngredientConsts(DefaultChild = typeof(EdamiSajt), IsIngrec = true)]
    public class Sajt : RecipeBase
    {
        public Sajt(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            throw new NotImplementedException();
        }
    }
}