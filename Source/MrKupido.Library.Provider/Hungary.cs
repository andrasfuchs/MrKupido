namespace MrKupido.Library.Provider
{
    public class Hungary : ProviderBase
    {
        public Hungary(string languageISO)
            : base(languageISO)
        {
            this.Country = "Hungary";
            this.CountryISO = "HU";
            this.CurrencyISO = "HUF";
        }
    }
}
