using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "pan")]
    [NameAlias("hun", "serpenyő")]
    public class Serpenyo : RoundContainer
    {
        public Serpenyo(float scale = 1.0f)
            : base(26.0f * scale, 4.0f)
        {
        }

        [NameAlias("eng", "bake", Priority = 200)]
        [NameAlias("hun", "kisüt", Priority = 200)]
        [NameAlias("hun", "süsd ki a(z) {N} az összes {0T} egyenként {1} percig")]
        public void KisutniOsszeset(IIngredient i, float timeForOne)
        {
            LastActionDuration = (uint)Math.Ceiling(i.PieceCount * timeForOne * 60);
        }
    }
}
