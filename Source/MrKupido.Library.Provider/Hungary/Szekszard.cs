namespace MrKupido.Library.Provider
{
    public class Szekszard : Hungary
    {
        public Szekszard(string languageISO)
            : base(languageISO)
        {
            this.Town = "Szekszárd";
            this.PostalCode = "7100";
        }
    }
}
