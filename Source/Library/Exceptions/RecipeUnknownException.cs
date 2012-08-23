using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class RecipeUnknownException : Exception
    {
        public RecipeUnknownException(string recipeClassName)
            : base("Recipe with class name '" + recipeClassName + "' has no implemenation.")
        { }
    }
}
