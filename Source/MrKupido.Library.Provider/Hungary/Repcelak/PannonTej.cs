using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class PannonTej : Repcelak
    {
        public PannonTej()
        {
            this.CompanyName = "Pannontej Zrt.";
            this.AddressLine = "Vörösmarty utca 1.";
            this.Website = new Uri("http://www.pannontej.hu");
        }
    }
}
