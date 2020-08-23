using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "flat small plate")]
    [NameAlias("hun", "lapos kis tányér")]

    [IconUriFragment("flat plate")]

    public class LaposKisTanyer : FlatContainer
    {
        public LaposKisTanyer()
            : base(19.0f, 19.0f, 2.0f)
        { }
    }
}
