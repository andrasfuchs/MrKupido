using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class CookedFoodParts : Dictionary<string, IIngredient>
    {
        public IIngredient this[int index]
        {
            get
            {
                return this[this.Keys.ToArray()[index]];
            }
        }
    }
}
