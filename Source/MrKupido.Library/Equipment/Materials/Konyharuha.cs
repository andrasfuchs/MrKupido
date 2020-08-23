using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "dish cloth")]
    [NameAlias("hun", "konyharuha")]
    public class Konyharuha : Material
    {
        public Konyharuha()
            : this(35.0f, 35.0f)
        {
        }

        public Konyharuha(float width, float length)
        {
        }
    }
}