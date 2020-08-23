using MrKupido.Library.Attributes;
using System;

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
        [NameAlias("eng", "bake the {0.Contents.} in the {}, spending {1} minutes on each (approx. {0.Contents.PieceCount.} pieces)")]
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
