using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "greaseproof paper")]
    [NameAlias("hun", "sütőpapír")]
    public class Sutopapir : Material
    {
        public Sutopapir()
            : this(30.0f, 34.0f)
        {
        }

        public Sutopapir(float width, float length)
        {
        }
    }
}