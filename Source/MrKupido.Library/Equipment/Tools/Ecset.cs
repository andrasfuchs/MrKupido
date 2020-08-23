using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "brush")]
    [NameAlias("hun", "ecset")]
    public class Ecset : Tool
    {
        [NameAlias("eng", "grease", Priority = 200)]
        [NameAlias("hun", "kiken", Priority = 200)]
        [NameAlias("eng", "grease the {1} with {0.Contents.}")]
        [NameAlias("hun", "kend ki a(z) {1T} a(z) {0.Contents.V}")]
        public void KikenniC(IIngredientContainer withC, IIngredientContainer c)
        {
            c.Add(withC.Contents);

            this.LastActionDuration = 180;
        }

        [IconUriFragment("spread")]

        [NameAlias("eng", "besmear", Priority = 200)]
        [NameAlias("hun", "megken", Priority = 200)]
        [NameAlias("eng", "besmear the {1.Contents.} with {0.Contents.}")]
        [NameAlias("hun", "kend meg a(z) {1.Contents.T} a(z) {0.Contents.V}")]
        public void MegkenniC(IIngredientContainer withC, IIngredientContainer c)
        {
            c.Add(withC.Contents);

            this.LastActionDuration = 300;
        }

        [IconUriFragment("spread")]

        [NameAlias("eng", "besmear", Priority = 200)]
        [NameAlias("hun", "megken", Priority = 200)]
        [NameAlias("eng", "besmear the {1.Contents.} with {0}")]
        [NameAlias("hun", "kend meg a(z) {1.Contents.T} a(z) {0V}")]
        public void MegkenniI(IIngredient withI, IIngredientContainer c)
        {
            c.Add(withI);

            this.LastActionDuration = 300;
        }

        [IconUriFragment("spread")]

        [NameAlias("eng", "daub", Priority = 200)]
        [NameAlias("hun", "beken", Priority = 200)]
        [NameAlias("eng", "daub the {1.Contents.} with {0}")]
        [NameAlias("hun", "kend be a(z) {1.Contents.T} a(z) {0V}")]
        public void BekenniI(IIngredient withI, IIngredientContainer c)
        {
            c.Add(withI);

            this.LastActionDuration = 300;
        }

    }
}
