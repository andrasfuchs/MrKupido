using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class CultureNotSupportedException : Exception
    {
        public CultureNotSupportedException(string recipeClassName, string culture)
            : base("The culture '" + culture + "' in recipe '" + recipeClassName + "' is not yet supported.")
        { }
    }
}