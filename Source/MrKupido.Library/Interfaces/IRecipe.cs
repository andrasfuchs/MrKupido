using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IRecipe : IIngredient
    {
        string Name { get; }

        EquipmentGroup SelectEquipment(float amount);

        PreparedIngredients Prepare(float amount, EquipmentGroup eg);

        CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg);

        void Serve(float amount, CookedFoodParts food, EquipmentGroup eg);

    }
}
