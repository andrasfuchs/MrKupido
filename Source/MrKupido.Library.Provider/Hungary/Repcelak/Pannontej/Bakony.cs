namespace MrKupido.Library.Provider
{
    public class Bakony : PannonTej
    {
        public Bakony(string languageISO)
            : base(languageISO)
        {
            this.BrandName = "Bakony";
            this.IconFilename = "bakony";
        }
    }
}
