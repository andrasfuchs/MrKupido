using System;
using System.Collections.Generic;
using System.Linq;

namespace MrKupido.Library
{
    [Serializable]
    public class CookedFoodParts : Dictionary<string, IIngredientContainer>
    {
        public IIngredientContainer this[int index]
        {
            get
            {
                return this[this.Keys.ToArray()[index]];
            }
        }
    }
}
