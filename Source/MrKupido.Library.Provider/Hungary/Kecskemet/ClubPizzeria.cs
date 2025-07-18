using MrKupido.Library.GeoLocation;
using System;

namespace MrKupido.Library.Provider
{
    public class ClubPizzeria : Kecskemet
    {
        public ClubPizzeria(string languageISO)
            : base(languageISO)
        {
            this.CompanyName = "Club pizzéria";
            this.AddressLine = "Jász utca 7.";
            this.WebsiteUrl = new Uri("http://www.clubpizzeria.com/");
            this.OrderPhoneNumber = "+36 (20) 559-0009";
            this.Location = new GeoCoordinate(46.908522, 19.67911);
        }
    }
}
