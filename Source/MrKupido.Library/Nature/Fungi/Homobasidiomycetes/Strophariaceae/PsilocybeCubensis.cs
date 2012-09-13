﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Nature
{
    [NameAlias("hun", "kubai badargomba")]
    [NameAlias("eng", "golden tops")]
    [NameAlias("eng", "cubes", Priority = 200)]
    [NameAlias("eng", "gold caps", Priority = 201)]
    [NameAlias("lat", "psilocybe cubensis")]

    [NatureSpecies]
    public class PsilocybeCubensis : Strophariaceae
    {
    }
}