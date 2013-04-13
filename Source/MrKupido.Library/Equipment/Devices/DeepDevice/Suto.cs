using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "oven")]
    [NameAlias("hun", "sütő")]
    public class Suto : DeepDevice
    {
        public Suto() : this(1.0f)
        {
        }

        public Suto(float scale)
            : base(38.0f * scale, 40.0f * scale, 28.0f * scale)
        {
        }

        [NameAlias("eng", "heat up", Priority = 200)]
        [NameAlias("hun", "előmelegít", Priority = 200)]
        [NameAlias("hun", "felmelegít", Priority = 201)]
        [NameAlias("hun", "melegítsd elő a(z) {T} {0} fokra")]
		[PassiveAction]
        public void Homerseklet(int temperature)
        {
            this.LastActionDuration = 60 * 15;
        }
    }
}