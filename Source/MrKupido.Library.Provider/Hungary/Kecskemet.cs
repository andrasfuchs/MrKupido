namespace MrKupido.Library.Provider
{
    public class Kecskemet : Hungary
    {
        public Kecskemet(string languageISO)
            : base(languageISO)
        {
            this.County = "Bács-kiskun";
            this.Town = "Kecskemét";
            this.PostalCode = "6000";
        }
    }
}
