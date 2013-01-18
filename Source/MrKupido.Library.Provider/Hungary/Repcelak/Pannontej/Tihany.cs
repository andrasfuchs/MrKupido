using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class Tihany : PannonTej
    {
        public Tihany(string languageISO)
            : base(languageISO)
        {
            this.BrandName = "Tihany";
        }
    }
}
