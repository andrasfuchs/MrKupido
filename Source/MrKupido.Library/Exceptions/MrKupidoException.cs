﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace MrKupido.Library
{
    [Serializable]
    public class MrKupidoException : Exception
    {
        public MrKupidoException(string message, params object[] args) : base(String.Format(message, args))
        {
            Trace.TraceError(String.Format(message, args));
        }

        public MrKupidoException(Exception innerException, string message, params object[] args)
            : base(String.Format(message, args), innerException)
        {
            Trace.TraceError(String.Format(message, args));
        }
    }
}
