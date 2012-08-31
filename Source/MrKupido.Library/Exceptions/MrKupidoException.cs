using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MrKupido.Library
{
    public class MrKupidoException : Exception
    {
        public MrKupidoException(string message, params object[] args) : base(String.Format(message, args))
        {
            Trace.TraceError(message);
        }

        public MrKupidoException(Exception innerException, string message, params object[] args)
            : base(String.Format(message, args), innerException)
        {
            Trace.TraceError(message);
        }
    }
}
