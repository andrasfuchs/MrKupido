using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "deep container")]
    [NameAlias("hun", "mély tároló")]
    public class DeepContainer : Container
    {
        public DeepContainer(float width, float height, float depth)
            : base(width, height, depth)
        {
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a következőket: ({0*}, )")]
        public void Berakni(params IIngredient[] ingredients)
        {
            this.contents.AddIngredients(ingredients);
            this.LastActionDuration = 90 * (uint)ingredients.Length;
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a(z) {0T}")]
        public void BerakniI(ISingleIngredient i)
        {
            this.contents.AddIngredients(i);
			this.LastActionDuration = 60 * (uint)i.PieceCount;
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a(z) {0.Contents.T}")]
        public void BerakniC(IIngredientContainer c)
        {
            this.contents.AddIngredients(c.Contents);
            this.LastActionDuration = 60 * (uint)c.Contents.PieceCount;
        }

        [NameAlias("eng", "pour into", Priority = 200)]
        [NameAlias("hun", "beleönt", Priority = 200)]
        [NameAlias("hun", "öntsd bele a(z) {B} a következőket: ({0*}, )")]
        public void Beonteni(params IIngredient[] ingredients)
        {
            this.contents.AddIngredients(ingredients);
            this.LastActionDuration = 15 * (uint)ingredients.Length;
        }

        [NameAlias("eng", "pour into", Priority = 200)]
        [NameAlias("hun", "beleönt", Priority = 200)]
        [NameAlias("hun", "öntsd bele a(z) {B} a(z) {0T}")]
        public void BeonteniI(ISingleIngredient i)
        {
            this.contents.AddIngredients(i);
            this.LastActionDuration = 15;
        }

        [NameAlias("eng", "pour into", Priority = 200)]
        [NameAlias("hun", "beleönt", Priority = 200)]
        [NameAlias("hun", "öntsd bele a(z) {B} a(z) {0.Contents.T}")]
        public void BeonteniC(IIngredientContainer c)
        {
            this.contents.AddIngredients(c.Contents);
            this.LastActionDuration = 15;
        }

        [NameAlias("eng", "pull out", Priority = 200)]
        [NameAlias("hun", "kivesz", Priority = 200)]
        [NameAlias("hun", "vedd ki a(z) {} tartalmát")]
        public IIngredient Kivenni()
        {
            IIngredient i = this.Contents;
            this.Empty();

            this.LastActionDuration = 30;

            return i;
        }
    }
}
