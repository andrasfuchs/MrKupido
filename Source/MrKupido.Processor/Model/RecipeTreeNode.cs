using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using System.Diagnostics;
using System.Reflection;
using MrKupido.Library;
using MrKupido.Library.Recipe;
using System.Web.Script.Serialization;

namespace MrKupido.Processor.Model
{
    public class RecipeTreeNode : TreeNode
    {
        public delegate EquipmentGroup SelectEquipmentDelegate(float amount);
        public delegate PreparedIngredients PrepareDelegate(float amount, EquipmentGroup eg);
        public delegate CookedFoodParts CookDelegate(float amount, PreparedIngredients preps, EquipmentGroup eg);
        public delegate void ServeDelegate(float amount, CookedFoodParts food, EquipmentGroup eg);

        [ScriptIgnore]
        public DateTime? ExpiresAt;

        [ScriptIgnore]
        public SelectEquipmentDelegate SelectEquipment;
        [ScriptIgnore]
        public PrepareDelegate Prepare;
        [ScriptIgnore]
        public CookDelegate Cook;
        [ScriptIgnore]
        public ServeDelegate Serve;

        [ScriptIgnore]
        private Dictionary<float, IIngredient[]> ingredientCache = new Dictionary<float, IIngredient[]>();
        [ScriptIgnore]
        private Dictionary<float, IDirection[]> directionCache = new Dictionary<float, IDirection[]>();

        [ScriptIgnore]
        public Type RecipeType { get; private set; }

        public bool IsImplemented = false;

        public RecipeTreeNode(Type recipeType)
            : base(recipeType)
        {
            char[] name = LongName.ToCharArray();
            name[0] = Char.ToUpper(name[0]);
            LongName = new string(name);

            RecipeType = recipeType; 

            MethodInfo mi = RecipeType.GetMethod("SelectEquipment");
            if (mi != null)
            {
                SelectEquipment = (SelectEquipmentDelegate)Delegate.CreateDelegate(typeof(SelectEquipmentDelegate), mi);
            }

            mi = RecipeType.GetMethod("Prepare");
            if (mi != null)
            {
                Prepare = (PrepareDelegate)Delegate.CreateDelegate(typeof(PrepareDelegate), mi);
            }

            mi = RecipeType.GetMethod("Cook");
            if (mi != null)
            {
                Cook = (CookDelegate)Delegate.CreateDelegate(typeof(CookDelegate), mi);
                IsImplemented = true;
            }

            mi = RecipeType.GetMethod("Serve");
            if (mi != null)
            {
                Serve = (ServeDelegate)Delegate.CreateDelegate(typeof(ServeDelegate), mi);
            }
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
