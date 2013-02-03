using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "icing sugar", Priority = 1)]
    [NameAlias("eng", "castor sugar")]
    [NameAlias("hun", "porcukor")]

    [IngredientConsts(IsIngrec = true)]
    public class PorCukor : Cukor
    {
        public PorCukor(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }

}
