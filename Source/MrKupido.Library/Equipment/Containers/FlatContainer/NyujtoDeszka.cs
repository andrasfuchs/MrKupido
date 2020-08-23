using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "providing board")]
    [NameAlias("hun", "nyújtódeszka")]
    public class NyujtoDeszka : FlatContainer
    {
        [NameAlias("hun", "{H} lévő tészta")]
        public new IIngredient Contents
        {
            get
            {
                return getContents();
            }
        }

        public NyujtoDeszka()
            : this(1.0f)
        { }

        public NyujtoDeszka(float scale)
            : base(40.0f * scale, 80.0f * scale, 1.0f)
        {
        }

        [NameAlias("eng", "roll out", Priority = 200)]
        [NameAlias("hun", "kinyújt", Priority = 200)]
        [NameAlias("eng", "roll the {0.Contents.} out on the {} to have a {1}-cm think pasta")]
        [NameAlias("hun", "nyújtsd ki {N} a(z) {0.Contents.T} {1} cm-esre")]
        public void NyujtaniC(IIngredientContainer c, float thicknessInCm)
        {
            this.Add(c.Contents);
            c.Empty();

            this.LastActionDuration = 180;
        }
    }
}
