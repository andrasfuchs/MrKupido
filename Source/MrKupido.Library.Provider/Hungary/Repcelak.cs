using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class Repcelak : Hungary
    {
        public Repcelak(string languageISO)
            : base(languageISO)
        {
            this.Town = "Répcelak";
            this.PostalCode = "9653";
        }
    }
}
