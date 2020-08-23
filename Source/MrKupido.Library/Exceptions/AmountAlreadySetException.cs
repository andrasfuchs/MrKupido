namespace MrKupido.Library
{
    public class AmountAlreadySetException : MrKupidoException
    {
        public AmountAlreadySetException(string recipeClassName, MeasurementUnit unit)
            : base("The amount of '" + unit.ToString() + "' in recipe '" + recipeClassName + "' was already set before.")
        { }
    }
}
