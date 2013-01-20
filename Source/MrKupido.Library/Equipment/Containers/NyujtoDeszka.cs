﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "providing board")]
    [NameAlias("hun", "nyújtódeszka")]
    public class NyujtoDeszka : Container
    {
        public NyujtoDeszka(float scale = 1.0f)
            : base(40.0f * scale, 80.0f * scale, 1.0f)
        {
        }

        [NameAlias("eng", "roll out", Priority = 200)]
        [NameAlias("hun", "kinyújt", Priority = 200)]
        [NameAlias("hun", "nyújtsd ki nyújtódeszkán a(z) {0} tartalmát {1} mm-esre")]
        public IIngredient Nyujtani(Container c, float thickness)
        {
            return c.Contents;
        }
    }
}
