using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;

namespace MrKupido.Processor
{
    [Serializable]
    public class ProcessorException : MrKupidoException
    {
        public ProcessorException(string message, params object[] args)
            : base(message, args)
        { }
    }
}
