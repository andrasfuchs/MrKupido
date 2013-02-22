using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        [NameAlias("eng", "put on", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("hun", "rakd rá a(z) {R} a következőket: ({0*}, )")]
        public void Rarakni(params IIngredient[] ingredients)
        {
            this.contents.AddIngredients(ingredients);
            this.LastActionDuration = 30 * (uint)ingredients.Length;
        }

        [NameAlias("eng", "put on", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("hun", "rakd rá a(z) {R} a(z) {0T}")]
        public void RarakniI(ISingleIngredient i)
        {
            this.contents.AddIngredients(i);
            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "put on", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("hun", "rakd rá a(z) {R} a(z) {0.Contents.T}")]
        public void RarakniC(IIngredientContainer c)
        {
            this.contents.AddIngredients(c.Contents);
            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "pour on", Priority = 200)]
        [NameAlias("hun", "ráönt", Priority = 200)]
        [NameAlias("hun", "öntsd rá a(z) {B} a(z) {0.Contents.T}")]
        public void RaonteniC(IIngredientContainer c)
        {
            this.contents.AddIngredients(c.Contents);
            this.LastActionDuration = 15;
        }

        [NameAlias("eng", "get off", Priority = 200)]
        [NameAlias("hun", "levesz", Priority = 200)]
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
