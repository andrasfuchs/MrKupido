using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "sea salt")]
    [NameAlias("hun", "tengeri só")]

    [KindOf(typeof(Nonliving))]
    public class TengeriSo : So
    {
        public TengeriSo(float amount)
            : base(amount)
        {
        }
    }
}