using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "pan")]
    [NameAlias("hun", "serpenyő")]
    public class Serpenyo : RoundContainer
    {
        public Serpenyo()
            : this(1.0f)
        { 
        }

        public Serpenyo(float scale)
            : base(26.0f * scale, 4.0f)
        {
        }

        [NameAlias("eng", "bake", Priority = 200)]
        [NameAlias("hun", "kisüt", Priority = 200)]
        [NameAlias("hun", "süsd ki a(z) {N} a(z) {0.Contents.T}, egyenként {1} percig (kb. {0.Contents.PieceCount.} db)")]
        public IIngredient KisutniOsszeset(IIngredientContainer i, float timeForOne)
        {
            IIngredient c = i.Contents;

            LastActionDuration = (uint)Math.Ceiling(c.PieceCount * timeForOne * 60);
            i.Empty();

            return c;
        }
    }
}
