using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "melegítő berendezés")]

    public class Heater : Device, IIngredientContainer
    {
        public float MinHeat;

        public float MaxHeat;

        public float HeatLevels;

        public Dimensions Dimensions { get; set; }

        public IIngredient Contents { get; set; }

        public Heater(float width, float height, float depth, int levels, float minHeat, float maxHeat, int heatlevels)
        {
            this.MinHeat = minHeat;
            this.MaxHeat = maxHeat;
            this.HeatLevels = heatlevels;

            this.Dimensions = new Dimensions(width, height, depth);
        }

        public bool Berakni(IIngredient ig)
        {
            return true;
        }

        public IIngredient Kivenni()
        {
            return this.Contents;
        }

        public void Varni(int minutes) { }

        public void Homerseklet(int temperature) { }
    }
}
