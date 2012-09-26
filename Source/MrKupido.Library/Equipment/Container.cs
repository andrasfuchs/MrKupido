using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "tároló")]
    [NameAlias("eng", "container")]

    public abstract class Container : EquipmentBase, IIngredientContainer
    {
        public Dimensions Dimensions { get; protected set; }

        public IIngredient Contents { get; set; }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        [NameAlias("hun", "rakd be a(z) {B} a {0T}")]
        public bool Berakni(IIngredient ig)
        {
            // TODO: return if it fits in
            this.Contents = ig;

            return true;
        }

        [NameAlias("hun", "vedd ki a(z) {} tartalmát")]
        public IIngredient Kivenni()
        {
            IIngredient i = this.Contents;
            this.Contents = null;

            return i;
        }

        [NameAlias("hun", "várj {0} percet")]
        public void Varni(int minutes) {}

        [NameAlias("hun", "fedd le a(z) {T} {0V}")]
        public void Lefedni(Material material = null) { }

        [NameAlias("hun", "vedd le a fedőt")]
        public void FedotLevenni() { }

        [NameAlias("hun", "öntsd le a folyadékot a(z) {L}")]
        public void FolyadekotLeonteni() { }
    }
}
