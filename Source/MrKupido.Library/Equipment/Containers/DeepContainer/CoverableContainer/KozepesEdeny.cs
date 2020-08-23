using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "middle-sized vessel")]
    [NameAlias("hun", "közepes méretű edény")]

    public class KozepesEdeny : Edeny
    {
        public KozepesEdeny()
            : base(1.0f)
        {
        }
    }
}
