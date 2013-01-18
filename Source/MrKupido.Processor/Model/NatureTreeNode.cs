using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class NatureTreeNode : TreeNode
    {
        public NatureNodeType Type { set; get; }

        public NatureTreeNode(Type natureClass, string languageISO) : base(natureClass, languageISO)
        {
            Type = NatureNodeType.None;

            foreach (object attr in natureClass.GetCustomAttributes(false))
            {
                if (attr.GetType().FullName == "MrKupido.Library.Attributes.NatureKingdomAttribute")
                {
                    if (Type == NatureNodeType.None) Type = NatureNodeType.Kingdom;
                    else throw new ProcessorException("Class '" + natureClass.Name + "' has more then one nature defining attributes.");
                }
            }
        }
    }
}
