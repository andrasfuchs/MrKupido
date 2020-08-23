using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "flat plate")]
    [NameAlias("hun", "lapos tányér")]
    public class LaposTanyer : FlatContainer
    {
        public LaposTanyer()
            : base(24.5f, 24.5f, 2.0f)
        { }
    }
}
