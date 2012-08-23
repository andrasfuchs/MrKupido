using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Processor.Model;

namespace MrKupido.Processor
{
    public interface ITreeBuilder
    {
        TreeNode Build(string natureNamespace);
    }
}
