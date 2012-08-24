using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "kősó")]

    public class KoSo : So
    {
        public KoSo(float amount)
            : base(amount)
        {
        }
    }
}