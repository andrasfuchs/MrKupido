using System;
using System.Device.Location;

namespace MrKupido.Library.Provider
{
    public class PizzaEsTesztahaz : Kecskemet
    {
        public PizzaEsTesztahaz(string languageISO)
            : base(languageISO)
        {
            this.CompanyName = "Pizza és tésztaház";
            this.AddressLine = "Lajtha László utca 2.";
            this.WebsiteUrl = new Uri("http://pizzaestesztahaz.hu/");
            this.OrderPhoneNumber = "+36 (70) 589-00-28";
            this.FeedbackPhoneNumber = "+36 (70) 589-00-28";
            this.Location = new GeoCoordinate(46.921435, 19.688058);
        }
    }
}
