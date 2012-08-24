using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class MrKupidoException : Exception
    {
        public MrKupidoException(string message) : base(message)
        { }
    }
}
