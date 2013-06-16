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
		[NameAlias("eng", "heat the {} up to {0} degrees (level: {-})")]
        [NameAlias("hun", "melegítsd elő a(z) {T} {0} fokra (fokozat: {-})")]
		[PassiveAction]
        public virtual int Homerseklet(int temperature)
        {
            this.LastActionDuration = 60 * 15;

			// source: http://diobisztro.blogspot.hu/2010/11/tudastar-gazsuto-versus-elektromos-suto.html

			if (temperature < 150)
			{
				return 0;
			}
			else
			{
				return (int)Math.Ceiling((temperature - 150.0) / 16.5);
			}
        }
    }
}