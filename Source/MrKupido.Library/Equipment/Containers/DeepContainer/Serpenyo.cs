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
    public class Serpenyo : DeepContainer
    {
        public Serpenyo()
            : this(1.0f)
        { 
        }

        public Serpenyo(float scale)
            : base(26.0f * scale, 26.0f * scale, 4.0f)
        {
        }

        [NameAlias("eng", "bake", Priority = 200)]
        [NameAlias("hun", "kisüt", Priority = 200)]
        [NameAlias("hun", "süsd ki a(z) {N} a(z) {0.Contents.T}, egyenként {1} percig (kb. {0.Contents.PieceCount.} db)")]
        public IIngredient KisutniOsszesetC(IIngredientContainer c, float timeForOne)
        {
            IIngredient i = c.Contents;

            LastActionDuration = (uint)Math.Ceiling(i.PieceCount * timeForOne * 60);
            c.Empty();

            return i;
        }
    }
}
