﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("eng", "barley")]
    [NameAlias("hun", "árpa")]

    [ContainsGluten]
    [NatureSpecies]
    public class HordeumVulgare : Poaceae
    {
    }
}
