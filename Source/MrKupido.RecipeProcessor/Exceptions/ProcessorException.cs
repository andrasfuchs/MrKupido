using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;

namespace MrKupido.Processor
{
    public class ProcessorException : MrKupidoException
    {
        public ProcessorException(string message)
            : base(message)
        { }
    }
}
