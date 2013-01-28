using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class ProviderBase
    {
        public string BrandName;
        public string CompanyName;
        
        public string Country;
        public string CountryISO;
        public string County;
        public string Town;
        public string AddressLine;
        public string PostalCode;
        public GeoCoordinate Location;

        public Uri FacebookUrl;   // like https://www.facebook.com/MaxKavezoEsPizzeria
        public Uri FourSquareUrl;   // like https://foursquare.com/v/club-pizzeria/4ce03717f8cdb1f727059612
        public Uri GooglePlusUrl;   // like https://plus.google.com/100649224126585302270
        public Uri WebsiteUrl;
        public Uri PriceDataUrl;
        public string IconFilename;

        public string OrderPhoneNumber;
        public string FeedbackPhoneNumber;
        public string FeedbackContactEmail;
        public string AdminContactEmail;

        public string OpenningHours;
        public string OrderingHours;
        
        public string Currency;
        public string CurrencyISO;

        public ProviderBase(string languageISOCode) {}
    }
}
