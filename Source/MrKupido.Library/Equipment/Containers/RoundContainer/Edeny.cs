using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "vessel")]
    [NameAlias("hun", "edény")]
    public class Edeny : RoundContainer
    {
        public Fedo Fedo = new Fedo();

        public Edeny(float scale = 1.0f)
            : base(7.0f * scale, 6.0f * scale)
        {
        }
    }
}
