namespace MrKupido.Library
{
    public class CultureNotSupportedException : MrKupidoException
    {
        public CultureNotSupportedException(string recipeClassName, string culture)
            : base("The culture '" + culture + "' in recipe '" + recipeClassName + "' is not yet supported.")
        { }
    }
}