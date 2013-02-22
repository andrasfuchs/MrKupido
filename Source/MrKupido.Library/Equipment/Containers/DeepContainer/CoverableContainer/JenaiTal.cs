using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "heatproof bowl")]
    [NameAlias("hun", "jénai tál")]

    public class JenaiTal : CoverableContainer
    {
        public Fedo Fedo = new Fedo();

        public JenaiTal()
            : this(26.8f, 11.3f)
        {
            // standard sizes [volume(l),r(mm),d(mm)]
            // 1,5 205 93
            // 2,5 249 113
            // 3,5 268 113
        }

        public JenaiTal(float diameter, float depth)
            : base(diameter, diameter, depth)
        {
        }
    }
}
