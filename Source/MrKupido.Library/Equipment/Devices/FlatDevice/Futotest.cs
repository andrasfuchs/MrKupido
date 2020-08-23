using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "radiator")]
    [NameAlias("hun", "fűtőtest")]
    public class Futotest : FlatDevice
    {
        public Futotest()
            : this(1.0f)
        {
        }

        public Futotest(float scale)
            : base(38.0f * scale, 40.0f * scale, 5.0f * scale)
        {
        }
    }

}
