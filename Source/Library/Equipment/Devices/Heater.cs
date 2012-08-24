using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Devices
{
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

        public void Berakni(IIngredient ig)
        {
        }

        public IIngredient Kivenni()
        {
            return this.Contents;
        }

        public void Varni(int minutes) { }

        public void Homerseklet(int temperature) { }
    }
}
