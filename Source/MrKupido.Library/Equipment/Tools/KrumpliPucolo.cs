﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "krumplipucoló")]

    public class KrumpliPucolo : Tool
    {
        public IIngredient Meghamozni(IIngredient i)
        {
            return i;
        }
    }
}