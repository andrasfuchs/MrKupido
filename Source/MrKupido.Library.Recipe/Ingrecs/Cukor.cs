using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "sugar")]
    [NameAlias("hun", "cukor")]

    [IngredientConsts(DefaultChild = typeof(KristalyCukor), GrammsPerLiter = 1000, IsAbstract = true, IsIngrec = true)]
    public class Cukor : RecipeBase
    {
        public Cukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}