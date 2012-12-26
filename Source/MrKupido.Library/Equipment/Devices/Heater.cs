using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "heater device")]
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

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "berak", Priority = 200)]
        [NameAlias("hun", "rakd be a(z) {B} a {0T}")]
        public bool Berakni(IIngredient ig)
        {
            this.LastActionDuration = 60;

            return true;
        }

        [NameAlias("eng", "pull out", Priority = 200)]
        [NameAlias("hun", "kivesz", Priority = 200)]
        [NameAlias("hun", "vedd ki a(z) {} tartalmát")]
        public IIngredient Kivenni()
        {
            this.LastActionDuration = 60;

            return this.Contents;
        }

        [NameAlias("eng", "wait", Priority = 200)]
        [NameAlias("hun", "vár", Priority = 200)]
        [NameAlias("hun", "várj {0} percet")]
        [PassiveAction]
        public void Varni(int minutes) 
        { 
            this.LastActionDuration = (uint)(minutes * 60);
        }

        [NameAlias("eng", "preheat", Priority = 200)]
        [NameAlias("hun", "előmelegít", Priority = 200)]
        [NameAlias("hun", "felmelegít", Priority = 201)]
        [NameAlias("hun", "állítsd a(z) {} hőmérsékletét {0} fokra")]
        public void Homerseklet(int temperature) 
        {
            this.LastActionDuration = 60;
        }
    }
}
