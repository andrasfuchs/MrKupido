using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "morzsolt főtt szalonna")]

    [IngredientConsts(IsIngrec = true)]
    public class MorzsoltFottKukorica : RecipeBase
    {
        public MorzsoltFottKukorica(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
            RecipeUnknown();
        }
    }
}