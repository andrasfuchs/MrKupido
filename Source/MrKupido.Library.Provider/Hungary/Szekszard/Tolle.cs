using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class Tolle : Szekszard
    {
        public Tolle(string languageISO)
            : base(languageISO)
        {
            this.BrandName = "Tolle";
            this.IconFilename = "tolle";
            this.CompanyName = "Tolnatej Zrt.";
            this.AddressLine = " Keselyűsi út 26.";
            this.WebsiteUrl = new Uri("http://www.tolle.hu/");
            this.FeedbackPhoneNumber = "+36 (74) 528-240";
        }
    }
}
