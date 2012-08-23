using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class RecipeBase : IngredientBase, IRecipe
    {
        public RecipeBase(float amount) : base(amount, MeasurementUnit.portion)
        {
        }

        public RecipeBase(float amount, MeasurementUnit unit) : base(amount, unit)
        {
        }
    }
}
