using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class Szekszard : Hungary
    {
        public Szekszard(string languageISO)
            : base(languageISO)
        {
            this.Town = "Szekszárd";
            this.PostalCode = "7100";
        }
    }
}
