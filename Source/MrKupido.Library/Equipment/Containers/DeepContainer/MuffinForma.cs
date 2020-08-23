using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "muffin form")]
    [NameAlias("hun", "muffin forma")]
    public class MuffinForma : DeepContainer
    {
        public MuffinForma()
            : base(24.5f, 24.5f, 4.0f)
        { }
    }
}
