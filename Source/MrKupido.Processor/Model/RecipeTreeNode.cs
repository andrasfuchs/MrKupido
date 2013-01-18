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
using MrKupido.Library.Attributes;

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

        public string Version { get; protected set; }

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
        public bool IsAbtract = false;
        public bool IsInline = false;
        public bool IsIngrec = false;
        public CommercialProductAttribute CommercialAttribute = null;

        public RecipeTreeNode(Type recipeType, string languageISO)
            : base(recipeType, languageISO)
        {
            int bracketStart = LongName.IndexOf('[');
            int bracketEnd = bracketStart >= 0 ? LongName.IndexOf(']', bracketStart) : -1;

            Version = bracketStart == -1 ? "" : LongName.Substring(bracketStart + 1, bracketEnd - bracketStart - 1);
            LongName = bracketStart == -1 ? LongName : LongName.Substring(0, bracketStart);

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
                IsImplemented = true;
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

            IngredientConstsAttribute[] ica = (IngredientConstsAttribute[])recipeType.GetCustomAttributes(typeof(IngredientConstsAttribute), false);
            if (ica.Length > 0)
            {
                IsAbtract = ica[0].IsAbstract;
                IsInline = ica[0].IsInline;
                IsIngrec = ica[0].IsIngrec;
            }

            CommercialProductAttribute[] commercialAttributes = (CommercialProductAttribute[])recipeType.GetCustomAttributes(typeof(CommercialProductAttribute), false);
            if (commercialAttributes.Length > 0)
            {
                CommercialAttribute = commercialAttributes[0];
            }
        }

        public string[] GetTags()
        {
            return new string[0];
        }
        
        public IIngredient[] GetIngredients(float amount)
        {
            if (!IsImplemented) return new IIngredient[0];

            lock (ingredientCache)
            {
                if (!ingredientCache.ContainsKey(amount))
                {
                    ingredientCache.Add(amount, RecipeAnalyzer.GenerateIngredients(this, 1.0f));
                }
            }

            return ingredientCache[amount];
        }

        public IEquipment[] GetEquipments(float amount)
        {
            return new MrKupido.Library.Equipment.EquipmentBase[0];
        }

        public IDirection[] GetDirections(float amount)
        {
            if (!IsImplemented) return new IDirection[0];

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
