using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "flat container")]
    [NameAlias("hun", "lapos tároló")]
    public class FlatContainer : Container
    {
        public FlatContainer(float width, float height, float depth)
            : base(width, height, depth)
        {
        }

        [NameAlias("eng", "put onto", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("eng", "put onto the {} the following: ({0*}, )")]
        [NameAlias("hun", "rakd rá a(z) {R} a következőket: ({0*}, )")]
        public void Rarakni(params IIngredient[] ingredients)
        {
            this.contents.AddIngredients(ingredients);
            this.LastActionDuration = 30 * (uint)ingredients.Length;
        }

        [NameAlias("eng", "put onto", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("eng", "put onto the {} the {0}")]
        [NameAlias("hun", "rakd rá a(z) {R} a(z) {0T}")]
        public void RarakniI(ISingleIngredient i)
        {
            this.contents.AddIngredients(i);
            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "put onto", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("eng", "put onto the {} the {0.Contents.}")]
        [NameAlias("hun", "rakd rá a(z) {R} a(z) {0.Contents.T}")]
        public void RarakniC(IIngredientContainer c)
        {
            this.contents.AddIngredients(c.Contents);
            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "pour into", Priority = 200)]
        [NameAlias("hun", "beleönt", Priority = 200)]
        [NameAlias("eng", "pour into the {} the {0.Contents.}")]
        [NameAlias("hun", "öntsd bele a(z) {B} a(z) {0.Contents.T}")]
        public void BeleonteniC(IIngredientContainer c)
        {
            this.contents.AddIngredients(c.Contents);
            this.LastActionDuration = 15;
        }

        [NameAlias("eng", "get off", Priority = 200)]
        [NameAlias("hun", "levesz", Priority = 200)]
        [NameAlias("eng", "get off the contents of {}")]
        [NameAlias("hun", "vedd le a(z) {} tartalmát")]
        public IIngredient Levenni()
        {
            IIngredient i = this.Contents;
            this.Empty();

            this.LastActionDuration = 30;

            return i;
        }
    }
}
