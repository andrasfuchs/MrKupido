using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class IngredientGroup : IIngredient
    {
        private IIngredient[] ingredients;

        private float amount;

        public float Amount
        {
            get
            {
                return amount;
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
            //this.amount = amount;
        }
    }
}
