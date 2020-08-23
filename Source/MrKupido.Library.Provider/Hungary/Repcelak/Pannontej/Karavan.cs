namespace MrKupido.Library.Provider
{
    public class Karavan : PannonTej
    {
        public Karavan(string languageISO)
            : base(languageISO)
        {
            this.BrandName = "Karaván";
            this.IconFilename = "karavan";
        }
    }

}
