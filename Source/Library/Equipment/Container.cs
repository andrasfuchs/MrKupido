using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "tárolók")]
    [NameAlias("eng", "containers")]

    public abstract class Container : EquipmentBase, IIngredientContainer
    {
        public Dimensions Dimensions { get; set; }

        public IIngredient Contents { get; set; }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        public bool Berakni(IIngredient ig)
        {
            // TODO: return if it fits in
            return true;
        }

        public IIngredient Kivenni()
        {
            return this.Contents;
        }

        public void Varni(int minutes) {}

        public void Lefedni(Material material = null) { }
        
        public void FedotLevenni() { }

        public void FolyadekotLeonteni() { }
    }
}
