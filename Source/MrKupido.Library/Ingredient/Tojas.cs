﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "tojás", Priority = 1)]
    [NameAlias("hun", "tyúktojás")]
    [NameAlias("eng", "egg")]

    [EggOf(typeof(GallusGallusDomesticus))]
    public class Tojas : IngredientBase
    {
        public Tojas(float amount)
            : base(amount, MeasurementUnit.piece)
        {
            //this.grammsPerPiece = 30;
        }
    }
}