using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class RecipeBase : IRecipe
    {
        public MeasurementUnit Unit
        {
            get
            {
                return MeasurementUnit.gramm;
            }
        }

        public float Cook(float pm)
        {
            throw new NotImplementedException();
        }
    }
}
