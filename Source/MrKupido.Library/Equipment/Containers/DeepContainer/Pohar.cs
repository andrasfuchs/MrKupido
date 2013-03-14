using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "glass")]
    [NameAlias("hun", "pohár")]
    public class Pohar : DeepContainer
    {
        public Pohar()
            : this(1.0f)
        {
        }

        public Pohar(float scale)
            : base(6.0f * scale, 6.0f * scale, 15.0f * scale)
        {
        }
    }
}
