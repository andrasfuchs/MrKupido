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
using MrKupido.Library.Ingredient;

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

        [ScriptIgnore]
        public string[] SearchStrings { get; private set; }

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

            SearchStrings = new string[0];
        }

        public string[] GetTags()
        {
            return new string[0];
        }

        public RuntimeIngredient[] GetIngredients(float amount, int multiplier)
        {
            List<RuntimeIngredient> result = new List<RuntimeIngredient>();

            IIngredient[] ingredients = GetRecipeIngredients(amount, multiplier);

            foreach (IngredientBase i in ingredients)
            {
                string classFullName = i.GetType().FullName;

                RecipeTreeNode rtn = Cache.Recipe[classFullName];
                if (rtn != null)
                {
                    result.Add(new RuntimeIngredient(i, rtn));
                    continue;
                }

                IngredientTreeNode itn = Cache.Ingredient[classFullName];
                if (itn != null)
                {
                    result.Add(new RuntimeIngredient(i, itn));
                    continue;
                }

                throw new MrKupidoException("Ingredient '{0}' was not found eighter in the ingredients or recipes trees.");
            }

            return result.ToArray();
        }

        private IIngredient[] GetRecipeIngredients(float amount, int multiplier)
        {
            float am = amount * multiplier;     
                   
            lock (ingredientCache)
            {
                if (!ingredientCache.ContainsKey(am))
                {
                    ingredientCache.Add(am, RecipeAnalyzer.GenerateIngredients(this, am));
                }
            }

            if (SearchStrings.Length == 0)
            {
                List<string> ingredientsForSearch = new List<string>();

                // add this class and its parent
                ingredientsForSearch.Add(this.NodeType + ":" + this.UniqueName);

                TreeNode currentParent = this.Parent;
                while (currentParent != null)
                {
                    ingredientsForSearch.Add(currentParent.NodeType + ":" + currentParent.UniqueName);
                    currentParent = currentParent.Parent;
                }

                // add all ingredients and all of their parents to the ingredientsForSearch collection
                foreach (IngredientBase ib in ingredientCache[am])
                {
                    IngredientBase currentIb = ib;
                    string ibTypeFullName = ib.GetType().FullName;

                    TreeNode itn = Cache.Ingredient.All.FirstOrDefault(tn => tn.ClassType.FullName == ibTypeFullName);
                    if (itn == null) itn = Cache.Recipe.All.FirstOrDefault(tn => tn.ClassType.FullName == ibTypeFullName);

                    while (itn != null)
                    {
                        ingredientsForSearch.Add(itn.NodeType + ":" + itn.UniqueName);

                        itn = itn.Parent;
                    }
                }

                // if this is a commercial product, let's inherit its parent's ingredientlist
                if ((this.CommercialAttribute != null) && (this.Parent != null) && (this.Parent is RecipeTreeNode))
                {
                    ingredientsForSearch.AddRange(((RecipeTreeNode)this.Parent).SearchStrings);
                }

                this.SearchStrings = ingredientsForSearch.ToArray();
            }



            return ingredientCache[am];
        }

        public IEquipment[] GetEquipments(float amount, int multiplier)
        {
            return new MrKupido.Library.Equipment.EquipmentBase[0];
        }

        public IDirection[] GetDirections(float amount, int multiplier)
        {
            float am = amount * multiplier;

            if (!directionCache.ContainsKey(am))
            {
                directionCache.Add(am, RecipeAnalyzer.GenerateDirections(this, am));
            }

            return directionCache[am];
        }

        public INutritionInfo[] GetNutritions(float amount, int multiplier)
        {
            return new NutritionInfo[0];
        }
    }
}
