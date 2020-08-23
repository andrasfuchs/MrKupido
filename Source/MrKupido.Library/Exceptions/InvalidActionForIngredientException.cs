namespace MrKupido.Library
{
    public class InvalidActionForIngredientException : MrKupidoException
    {
        public InvalidActionForIngredientException(string actionName, IIngredient i)
            : base("The action '" + actionName + "' on ingredient '" + i.Name + "' (" + i.Unit.ToString() + ") is invalid.")
        { }
    }
}
