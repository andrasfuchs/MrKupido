namespace MrKupido.Library
{
    public interface ITag
    {
        float Match(ITreeNode r);

        bool IsMatch(ITreeNode r);
    }
}
