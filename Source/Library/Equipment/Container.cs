using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    public class Container : IEquipment
    {
        public IngredientGroup Contents { get; set; }
    }
}
