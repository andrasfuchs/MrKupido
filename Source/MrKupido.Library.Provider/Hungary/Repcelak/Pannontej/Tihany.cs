namespace MrKupido.Library.Provider
{
    public class Tihany : PannonTej
    {
        public Tihany(string languageISO)
            : base(languageISO)
        {
            this.BrandName = "Tihany";
            this.IconFilename = "tihany";
        }
    }
}
