using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using Mono.Cecil;
using System.Diagnostics;
using MrKupido.Library.Recipe;
using System.Reflection;

namespace MrKupido.Processor.Model
{
    public class RecipeTreeNode : TreeNode
    {
        public delegate EquipmentGroup SelectEquipmentDelegate(float amount);
        public delegate PreparedIngredients PrepareDelegate(float amount, EquipmentGroup eg);
        public delegate CookedFoodParts CookDelegate(float amount, PreparedIngredients preps, EquipmentGroup eg);
        public delegate void ServeDelegate(float amount, CookedFoodParts food, EquipmentGroup eg);

        public DateTime? ExpiresAt;

        public SelectEquipmentDelegate SelectEquipment;
        //public RecipeMethod SelectEquipmentMethod;
        public PrepareDelegate Prepare;
        //public RecipeMethod PrepareMethod;
        public CookDelegate Cook;
        //public RecipeMethod CookMethod;
        public ServeDelegate Serve;
        //public RecipeMethod ServeMethod;

        public Type RecipeType { get; private set; }
        //public RecipeBase StandardInstance { get; private set; }

        public RecipeTreeNode(Type recipeType)
            : base(recipeType)
        {
            RecipeType = recipeType;
            //StandardInstance = (RecipeBase)RecipeType.DefaultConstructor(0.0f);

            try
            {
                SelectEquipment = (SelectEquipmentDelegate)Delegate.CreateDelegate(typeof(SelectEquipmentDelegate), RecipeType.GetMethod("SelectEquipment"));
            }
            catch (Exception) { }

            try
            {
                Prepare = (PrepareDelegate)Delegate.CreateDelegate(typeof(PrepareDelegate), RecipeType.GetMethod("Prepare"));
            }
            catch (Exception) { }

            try
            {
                Cook = (CookDelegate)Delegate.CreateDelegate(typeof(CookDelegate), RecipeType.GetMethod("Cook"));
            }
            catch (Exception) { }

            try
            {
                Serve = (ServeDelegate)Delegate.CreateDelegate(typeof(ServeDelegate), RecipeType.GetMethod("Serve"));
            }
            catch (Exception) { }

        }
    }
}
