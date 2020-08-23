using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "vessel")]
    [NameAlias("hun", "edény")]

    public class Edeny : CoverableContainer
    {
        public Fedo Fedo = new Fedo();

        public Edeny()
            : this(1.0f)
        {
        }

        public Edeny(float scale)
            : base(14.0f * scale, 14.0f * scale, 6.0f * scale)
        {
        }
    }
}
