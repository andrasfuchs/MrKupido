using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public class TagCache : BaseCache
    {
        public new TagTreeNode this[string name]
        {
            get
            {
                return (TagTreeNode)base[name];
            }
        }

        public void Initialize(string languageISO)
        {
            if (Indexer != null) return;

            this.language = languageISO;

            TagTreeNode root = TreeNode.BuildTree(Cache.Assemblies, t => new TagTreeNode(t, languageISO), typeof(MrKupido.Library.Tag.TagBase));
            Indexer = new Indexer(root, languageISO);

            WasInitialized = true;
        }
    }
}
