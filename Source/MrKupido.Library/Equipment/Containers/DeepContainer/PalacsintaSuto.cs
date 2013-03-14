using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "griddle")]
    [NameAlias("hun", "palacsintasütő")]
    public class PalacsintaSuto : DeepContainer
    {
        public PalacsintaSuto()
            : this(1.0f)
        { 
        }

		public PalacsintaSuto(float scale)
            : base(30.0f * scale, 30.0f * scale, 1.5f)
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
