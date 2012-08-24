using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    public abstract class Container : IEquipment, IIngredientContainer
    {
        public Dimensions Dimensions { get; set; }

        public IIngredient Contents { get; set; }

        public Container(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        public void Berakni(IIngredient ig)
        {
        }

        public IIngredient Kivenni()
        {
            return this.Contents;
        }

        public void Varni(int minutes) {}

        public void Lefedni(Material material = null) { }
        
        public void FedotLevenni() { }
    }
}
