using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "vanilla sugar")]
    [NameAlias("hun", "vaníliás cukor")]

    [IngredientConsts(IsIngrec = true)]
    public class VaniliasCukor : Cukor
    {
        public VaniliasCukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
