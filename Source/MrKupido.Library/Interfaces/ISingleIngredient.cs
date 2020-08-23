namespace MrKupido.Library
{
    public interface ISingleIngredient : IIngredient
    {
        IngredientState State { get; set; }

        int PieceCount { get; set; }
    }
}
