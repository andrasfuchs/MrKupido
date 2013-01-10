using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "füstölt szalonna")]

    [IngredientConsts(GrammsPerPiece=3.0f, IsIngrec = true)]
    public class FustoltSzalonna : RecipeBase
    {
        public FustoltSzalonna(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}
