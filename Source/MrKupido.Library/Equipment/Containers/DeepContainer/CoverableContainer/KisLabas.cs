using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "small casserole")]
    [NameAlias("hun", "kis lábas")]
    public class KisLabas : CoverableContainer
    {
        public KisLabas()
            : this(1.0f)
        {
        }

        public KisLabas(float scale)
            : base(15.0f * scale, 15.0f * scale, 10.0f)
        {
        }
    }
}
