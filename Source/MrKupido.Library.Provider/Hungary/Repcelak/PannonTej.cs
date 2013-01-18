using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class PannonTej : Repcelak
    {
        public PannonTej(string languageISO)
            : base(languageISO)
        {
            this.CompanyName = "Pannontej Zrt.";
            this.AddressLine = "Vörösmarty utca 1.";
            this.WebsiteUrl = new Uri("http://www.pannontej.hu");
        }
    }
}
