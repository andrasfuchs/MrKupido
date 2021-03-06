﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "recipe")]
    [NameAlias("hun", "recept")]

    [IngredientConsts(IsAbstract = true)]
    public class RecipeBase : SingleIngredient, IRecipe
    {
        public float Portion { get; private set; }

        public RecipeBase(float amount) : this(amount, MeasurementUnit.portion)
        {
            Portion = amount;
        }

        public RecipeBase(float amount, MeasurementUnit unit) : base(amount, unit)
        {
            // TODO: portion size calculation
            Portion = amount;


            //EquipmentGroup eg = SelectEquipment(amount);

            //PreparedIngredients preps = Prepare(amount, eg);

            //CookedFoodParts food = Cook(amount, preps, eg);

            //Serve(amount, food, eg);
        }

        //public static EquipmentGroup SelectEquipment(float amount)
        //{
        //    throw new NotImplementedException();
        //}

        //public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        //{
        //    throw new NotImplementedException();
        //}

        //public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        //{
        //    throw new NotImplementedException();
        //}

        //public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        //{
        //    throw new NotImplementedException();
        //}

        public void RecipeUnknown()
        {
            // check if any descendant of this is a commercial product
            // a, if it is, then do not throw the Exception, only trace a warning (because it would be nice to have the recipe, but we can buy the product, so it's fine)
            // b, if none of them are, throw the Exception, because then we can't make this recipe

            List<Type> typesToCheck = new List<Type>();
            typesToCheck.Add(this.GetType());
            //typesToCheck.AddRange(this.GetType().GetDescendants());

            foreach (Type desc in typesToCheck)
            {
                foreach (object attr in desc.GetCustomAttributes(typeof(CommercialProductAttribute), false))
                {
                    if (desc != this.GetType())
                    {
                        Trace.TraceWarning("The recipe of '{0}' is unknown, so it is available only as commercial product. Please consider adding its recipe to the library.", this.GetType().Name);
                    }
                    return;
                }
            }

            //TODO: re-enable this exception
            //throw new RecipeUnknownException(this.GetType().Name);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
