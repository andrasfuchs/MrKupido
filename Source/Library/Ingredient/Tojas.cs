using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [EggOf(typeof(GallusGallusDomesticus))]
    public class Tojas : IngredientBase
    {
        public Tojas(float amount)
            : base(amount, MeasurementUnit.piece)
        {
            this.grammsPerPiece = 30;
        }
    }
}
