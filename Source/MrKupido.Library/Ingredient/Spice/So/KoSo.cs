﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "rock salt")]
    [NameAlias("hun", "kősó")]

    [KindOf(typeof(Nonliving))]
    public class KoSo : So
    {
        public KoSo(float amount)
            : base(amount)
        {
        }
    }
}