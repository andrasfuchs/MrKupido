using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "cooker")]
    [NameAlias("hun", "tűzhely")]
    public class Tuzhely : FlatDevice
    {
        public Tuzhely()
            : this(1.0f)
        {
        }

        public Tuzhely(float scale)
            : base(38.0f * scale, 40.0f * scale, 5.0f * scale)
        {
        }

        [NameAlias("eng", "heat up", Priority = 200)]
        [NameAlias("hun", "felmelegít", Priority = 201)]
        [NameAlias("eng", "set the heat on the {} to {0} degrees")]
        [NameAlias("hun", "állítsd a(z) {} hőmérsékletét {0} fokra")]
        public void Homerseklet(int temperature)
        {
            this.LastActionDuration = 60;
        }
    }
}
