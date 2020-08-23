using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "tin foil")]
    [NameAlias("hun", "alufólia")]
    public class Alufolia : Material
    {
        public Alufolia()
            : this(29.0f, 1000.0f)
        {
        }

        public Alufolia(float width, float length)
        {
        }
    }
}