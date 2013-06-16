using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "tray")]
    [NameAlias("hun", "tepsi")]
    public class Tepsi : CoverableContainer
    {
        public Tepsi()
            : this(1.0f)
        {
        }

        public Tepsi(float scale)
            : base(30.0f * scale, 34.0f * scale, 2.0f)
        {
        }

        [NameAlias("eng", "cotton", Priority = 200)]
        [NameAlias("hun", "kibélel", Priority = 200)]
		[NameAlias("eng", "cotton the {} with the {0}")]
        [NameAlias("hun", "béleld ki a(z) {T} {0V}")]
        public void Kibelelni(Material material)
        {
            this.LastActionDuration = 120;
        }
    }
}
