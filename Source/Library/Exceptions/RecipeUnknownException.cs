using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class RecipeUnknownException : MrKupidoException
    {
        public RecipeUnknownException(string recipeClassName)
            : base("Recipe with class name '" + recipeClassName + "' has no implemenation.")
        { }
    }
}
