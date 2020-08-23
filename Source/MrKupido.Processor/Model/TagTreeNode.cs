using System;

namespace MrKupido.Processor.Model
{
    public class TagTreeNode : TreeNode
    {
        public TagTreeNode(Type tagClass, string languageISO)
            : base(tagClass, languageISO)
        {
        }
    }
}
