using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "device")]
    [NameAlias("hun", "berendezés")]
    public class Device : EquipmentBase
    {
        public Dimensions Dimensions { get; protected set; }

        public int Id { get; set; }

        protected List<IIngredientContainer> contents = new List<IIngredientContainer>();
        protected IIngredientContainer Contents
        {
            get 
            {
                if (contents.Count == 0) return null;
                
                return contents.Last(); 
            }
        }

        public int AveragePowerConsumption { get; protected set; } 

        public Device(float width, float height, float depth)
        {
            this.Dimensions = new Dimensions(width, height, depth);
        }

        [NameAlias("eng", "wait", Priority = 200)]
        [NameAlias("hun", "vár", Priority = 200)]
		[NameAlias("eng", "wait {0}")]
        [NameAlias("hun", "várj {0T}")]
        [PassiveAction]
        public void Varni(Quantity duration)
        {
			this.LastActionDuration = (uint)(duration.GetAmount(MeasurementUnit.minute) * 60);
        }
    }
}
