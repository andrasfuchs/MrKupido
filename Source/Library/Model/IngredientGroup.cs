using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class IngredientGroup : IIngredient
    {
        private IIngredient[] ingredients;

        public ShoppingListCategory Category 
        { 
            get
            {
                return ShoppingListCategory.Mixed;
            }
        }

        public MeasurementUnit Unit 
        { 
            get
            {
                return MeasurementUnit.gramm;
            }        
        }

        public int Count
        {
            get
            {
                return ingredients.Length;
            }
        }

        public IngredientGroup(IIngredient[] ingredients)
        {
        }

        public float GetAmount()
        {
            return 0.0f;
        }

        public float GetAmount(MeasurementUnit unit)
        {
            return 0.0f;
        }
    }
}
