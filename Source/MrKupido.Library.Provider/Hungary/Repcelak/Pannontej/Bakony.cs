using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class Bakony : PannonTej
    {
        public Bakony(string languageISO)
            : base(languageISO)
        {
            this.BrandName = "Bakony";
        }
    }
}
