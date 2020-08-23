using MrKupido.Library;
using System;

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
