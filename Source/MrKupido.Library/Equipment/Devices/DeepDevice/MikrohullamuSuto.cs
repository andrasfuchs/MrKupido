using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "microwave oven")]
    [NameAlias("hun", "mikró", Priority = 1)]
    [NameAlias("hun", "mikrohullámú sütő")]
    public class MikrohullamuSuto : DeepDevice
    {
        public MikrohullamuSuto() : this(1.0f)
        {
        }

        public MikrohullamuSuto(float scale)
            : base(32.0f * scale, 20.0f * scale, 32.0f * scale)
        {
        }

        [NameAlias("eng", "melt", Priority = 200)]
        [NameAlias("hun", "megolvaszt", Priority = 200)]
        [NameAlias("eng", "melt the {0}")]
        [NameAlias("hun", "olvaszd meg a(z) {0T}")]
        [PassiveAction]
        public virtual void Megolvasztani(ISingleIngredient i)
        {
            i.State |= IngredientState.Olvasztott;

            this.LastActionDuration = 180;
        }
    }
}