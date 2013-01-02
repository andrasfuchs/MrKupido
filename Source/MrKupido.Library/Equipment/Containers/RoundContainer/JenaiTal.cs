using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "heatproof bowl")]
    [NameAlias("hun", "jénai tál")]

    public class JenaiTal : RoundContainer
    {
        public Fedo Fedo = new Fedo();

        public JenaiTal(float radius, float depth) : base(radius, depth)
        {
            // standard sizes [volume(l),r(cm),d(cm)]
            // 1,5 205 93
            // 2,5 249 113
            // 3,5 268 113
        }
    }
}
