using MrKupido.Library.Attributes;
using System;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "skillet", Priority = 1)]
    [NameAlias("eng", "pan", Priority = 2)]
    [NameAlias("eng", "frying pan")]
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
        [NameAlias("eng", "bake the {0.Contents.} in the {}, spending {1} minutes on each (approx. {0.Contents.PieceCount.} pieces), and put them into {2}")]
        [NameAlias("hun", "süsd ki a(z) {N} a(z) {0.Contents.T}, egyenként {1} percig (kb. {0.Contents.PieceCount.} db), majd helyezd őket a {2R}")]
        public void KisutniOsszesetC(IIngredientContainer c, float timeForOne, IIngredientContainer cTo)
        {
            cTo.Add(c.Contents);
            c.Empty();

            LastActionDuration = (uint)Math.Ceiling(cTo.Contents.PieceCount * timeForOne * 60 + cTo.Contents.PieceCount * 30);
        }
    }
}
