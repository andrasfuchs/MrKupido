using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "fridge")]
    [NameAlias("hun", "hűtő", Priority = 1)]
    [NameAlias("hun", "hűtőgép")]
    public class Hutogep : Device
    {
        public Dimensions Dimensions { get; set; }

        public IIngredient Contents { get; set; }

        public Hutogep() : this(50,60,40)
        {
        }

        public Hutogep(float width, float height, float depth)
        {
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
    }
}
