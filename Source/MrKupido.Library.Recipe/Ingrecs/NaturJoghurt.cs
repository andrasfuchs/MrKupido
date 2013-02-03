using System;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "plain yogurt")]
    [NameAlias("hun", "natúr joghurt")]

    [IngredientConsts(IsIngrec = true)]
    public class NaturJoghurt : RecipeBase
    {
        public NaturJoghurt(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}