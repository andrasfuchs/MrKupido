using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "csirkemell")]
    [NameAlias("eng", "chicken breast")]

    [PartOf(typeof(GallusGallusDomesticus))]
    class Csirkemell : IngredientBase
    {
        public Csirkemell(float amount)
            : base(amount, MeasurementUnit.gramm)
        {
        }
    }
}
