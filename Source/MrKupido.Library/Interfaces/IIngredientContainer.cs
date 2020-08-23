namespace MrKupido.Library
{
    public interface IIngredientContainer
    {
        Dimensions Dimensions { get; }

        int Id { get; set; }

        IIngredient Contents { get; }

        void Varni(Quantity duration);

        string GetName(string languageISO);

        void Add(IIngredient i);
        void AddRange(IIngredient[] i);
        void Empty();
    }
}

