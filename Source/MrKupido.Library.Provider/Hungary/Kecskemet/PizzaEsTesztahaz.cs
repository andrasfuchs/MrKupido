using MrKupido.Library;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKupido.Library.Provider
{
    public class PizzaEsTesztahaz : Kecskemet
    {
        public PizzaEsTesztahaz()
        {
            this.CompanyName = "Pizza és tésztaház";
            this.AddressLine = "Lajtha László utca 2.";
            this.Website = new Uri("http://pizzaestesztahaz.hu/");
            this.OrderPhoneNumber = "+36 (70) 589-00-28";
            this.FeedbackPhoneNumber = "+36 (70) 589-00-28";
            this.Location = new GeoCoordinate(46.921435, 19.688058);
        }
    }
}
