using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "soup plate")]
    [NameAlias("hun", "mély tányér")]
    public class MelyTanyer : DeepContainer
    {
        public MelyTanyer()
            : base(24.5f, 24.5f, 4.0f)
        { }
    }
}
