using MrKupido.Library.Attributes;

namespace MrKupido.Library.Tag
{
    [NameAlias("eng", "tag")]
    [NameAlias("hun", "címke")]
    public class TagBase : NamedObject, ITag
    {
        public virtual float Match(ITreeNode r)
        {
            return 0.0f;
        }

        public virtual bool IsMatch(ITreeNode r)
        {
            return Match(r) == 1.0f;
        }
    }
}
