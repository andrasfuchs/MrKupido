using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Provider
{
    public class Tolle : Szekszard
    {
        public Tolle()
        {
            this.BrandName = "Tolle";
            this.CompanyName = "Tolnatej Zrt.";
            this.AddressLine = " Keselyűsi út 26.";
            this.Website = new Uri("http://www.tolle.hu/");
            this.FeedbackPhoneNumber = "+36 (74) 528-240";
        }
    }
}
