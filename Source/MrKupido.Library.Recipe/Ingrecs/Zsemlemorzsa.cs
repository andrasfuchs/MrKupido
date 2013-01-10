using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "zsemlemorzsa")]
    [NameAlias("hun", "kenyérmorzsa", Priority = 200)]
    [NameAlias("eng", "breadcrumb")]

    [IngredientConsts(IsIngrec = true)]
    public class Zsemlemorzsa : RecipeBase
    {
        public Zsemlemorzsa(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}