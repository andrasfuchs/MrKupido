using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{    
    [NameAlias("hun", "sonka")]
    [NameAlias("hun", "disznó combjából származó hús", Priority = 200)]

    [PartOf(typeof(SusScrofaDomestica))]
    public class Sonka : IngredientBase
    {
        public Sonka(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}