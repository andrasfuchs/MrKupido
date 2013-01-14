using MrKupido.Library;
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MrKupido.Library.Provider
{
    public class ClubPizzeria : Kecskemet
    {
        public ClubPizzeria()
        {
            this.CompanyName = "Club pizzéria";
            this.AddressLine = "Jász utca 7.";
            this.Website = new Uri("http://www.clubpizzeria.com/");
            this.OrderPhoneNumber = "+36 (20) 559-0009";
            this.Location = new GeoCoordinate(46.908522, 19.67911);
        }
    }
}
