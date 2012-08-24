using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class RecipeBase : IngredientBase, IRecipe
    {
        public RecipeBase(float amount) : this(amount, MeasurementUnit.portion)
        {
        }

        public RecipeBase(float amount, MeasurementUnit unit) : base(amount, unit)
        {
            EquipmentGroup eg = SelectEquipment(amount);

            PreparedIngredients preps = Prepare(amount, eg);

            CookedFoodParts food = Cook(amount, preps, eg);

            Serve(amount, food, eg);
        }

        public virtual EquipmentGroup SelectEquipment(float amount)
        {
            throw new NotImplementedException();
        }

        public virtual PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            throw new NotImplementedException();
        }

        public virtual CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            throw new NotImplementedException();
        }

        public virtual void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            throw new NotImplementedException();
        }
    }
}
