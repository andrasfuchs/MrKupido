using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using System.Diagnostics;
using System.Reflection;
using MrKupido.Library;
using MrKupido.Library.Recipe;

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
        public PrepareDelegate Prepare;
        public CookDelegate Cook;
        public ServeDelegate Serve;

        private Dictionary<float, IIngredient[]> ingredientCache = new Dictionary<float, IIngredient[]>();
        private Dictionary<float, IDirection[]> directionCache = new Dictionary<float, IDirection[]>();

        public Type RecipeType { get; private set; }

        public RecipeTreeNode(Type recipeType)
            : base(recipeType)
        {
            RecipeType = recipeType;

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

        public string[] GetTags()
        {
            return new string[0];
        }
        
        public IIngredient[] GetIngredients(float amount)
        {
            if (!ingredientCache.ContainsKey(amount))
            {
                ingredientCache.Add(amount, RecipeAnalyzer.GenerateIngredients(this, 1.0f));
            }

            return ingredientCache[amount];
        }

        public IEquipment[] GetEquipments(float amount)
        {
            return new MrKupido.Library.Equipment.EquipmentBase[0];
        }

        public IDirection[] GetDirections(float amount)
        {
            if (!directionCache.ContainsKey(amount))
            {
                directionCache.Add(amount, RecipeAnalyzer.GenerateDirections(this, 1.0f));
            }

            return directionCache[amount];
        }

        public INutritionInfo[] GetNutritions(float amount)
        {
            return new NutritionInfo[0];
        }
    }
}
