namespace MrKupido.Library
{
    public class RecipeUnknownException : MrKupidoException
    {
        public RecipeUnknownException(string recipeClassName)
            : base("Recipe with class name '" + recipeClassName + "' has no implemenation.")
        { }
    }
}
