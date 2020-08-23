using MrKupido.Library;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Recipe;
using MrKupido.Processor.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MrKupido.Processor
{
    public class RecipeAnalyzer
    {
        private static string currentRecipeUniqieName = "";
        private static Dictionary<string, List<IngredientBase>> ingredients = new Dictionary<string, List<IngredientBase>>();
        private static List<string> seenIngredients = new List<string>();
        private static Dictionary<int, int> containerIds = new Dictionary<int, int>();
        private static List<RecipeDirection> directions = new List<RecipeDirection>();
        private static List<IEquipment> equipment = new List<IEquipment>();
        private static RecipeStage stage = RecipeStage.Unknown;
        private static List<object> tempParameters = new List<object>();
        private static bool ignoreDirections = false;
        private static string languageISO = "";

        public static IDictionary<string, IIngredient[]> GenerateIngredients(RecipeTreeNode rtn, float amount)
        {
            ClearStatics();
            //directions.Clear();

            ignoreDirections = true;

            RunRecipe(rtn, amount);


            IDictionary<string, IIngredient[]> result = new Dictionary<string, IIngredient[]>();
            foreach (string key in ingredients.Keys)
            {
                result.Add(key, ingredients[key].ToArray());
            }

            return result;
        }

        public static object NewIngredient(object returnedObject)
        {
            RecipeBase rb = null;
            RecipeTreeNode rtn = null;

            if (returnedObject is RecipeBase)
            {
                rb = ((RecipeBase)returnedObject);
                string rbTypeName = rb.GetType().Name;
                rtn = Cache.Recipe.All.FirstOrDefault(tn => tn.ClassName == rbTypeName);
            }

            if (returnedObject is IngredientBase)
            {
                IngredientBase ib = (IngredientBase)((IngredientBase)returnedObject).Clone();

                lock (ingredients)
                {
                    if (!ingredients.ContainsKey(currentRecipeUniqieName))
                    {
                        ingredients.Add(currentRecipeUniqieName, new List<IngredientBase>());
                    }

                    IngredientBase item = ingredients[currentRecipeUniqieName].FirstOrDefault(i => (i.Name == ib.Name) && (i.Unit == ib.Unit));

                    if (item == null)
                    {
                        if ((rtn == null) || (!rtn.IsInline))
                        {
                            ingredients[currentRecipeUniqieName].Add(ib);
                        }
                    }
                    else item.Add(ib);
                }
            }

            if (returnedObject is RecipeBase)
            {
                // if the recipe is inline then we need to run it (without the serving part)
                if ((rtn != null) && (rtn.IsInline))
                {
                    string parentRecipeUniqueName = currentRecipeUniqieName;
                    currentRecipeUniqieName = rtn.UniqueName;

                    //RunRecipe(rtn, rb.Portion);
                    if (rtn.SelectEquipment != null)
                    {
                        stage = RecipeStage.EquipmentSelection;
                        EquipmentGroup eg = rtn.SelectEquipment(rb.Portion);
                        if (rtn.Prepare != null)
                        {
                            stage = RecipeStage.Preparation;
                            PreparedIngredients preps = rtn.Prepare(rb.Portion, eg);
                            if (rtn.Cook != null)
                            {
                                stage = RecipeStage.Cooking;
                                CookedFoodParts cfp = rtn.Cook(rb.Portion, preps, eg);
                            }
                        }
                    }

                    currentRecipeUniqieName = parentRecipeUniqueName;
                }
            }

            return returnedObject;
        }

        public static void DirectionGeneratorAfter(string assemblyFullName, string methodFullName, object[] parameters, object result)
        {
            if (ignoreDirections) return;

            SetIds(new object[] { result });

            if ((parameters[0] is IEquipment) && (tempParameters[0] is IEquipment))
            {
                uint duration = ((IEquipment)parameters[0]).LastActionDuration;
                ((IEquipment)tempParameters[0]).LastActionDuration = duration;
                ((IEquipment)parameters[0]).LastActionDuration = 0; // I have no idea why is this here...

                if (duration == 0) Trace.TraceWarning("The duration of the action '{0}' is not set.", methodFullName);
            }

            directions.Add(new RecipeDirection(languageISO, assemblyFullName, methodFullName, tempParameters.ToArray(), result, stage, 1, seenIngredients, equipment.ToArray()));
        }

        public static void DirectionGeneratorBefore(string assemblyFullName, string methodFullName, object[] parameters)
        {
            if (ignoreDirections) return;

            tempParameters.Clear();

            SetIds(parameters);
            RecordsEquipment(parameters);

            foreach (object p in parameters)
            {
                if (p is ICloneable)
                {
                    tempParameters.Add(((ICloneable)p).Clone());
                }
                else
                {
                    tempParameters.Add(p);
                }
            }
        }

        private static void SetIds(object[] parameters)
        {
            foreach (object p in parameters)
            {
                if (p is IIngredientContainer)
                {
                    if (!containerIds.ContainsKey(p.GetHashCode()))
                    {
                        containerIds.Add(p.GetHashCode(), containerIds.Count + 1);
                    }

                    ((IIngredientContainer)p).Id = containerIds[p.GetHashCode()];
                }

                if (p is IIngredientGroup)
                {
                    if (!containerIds.ContainsKey(p.GetHashCode()))
                    {
                        containerIds.Add(p.GetHashCode(), containerIds.Count + 1);
                    }

                    ((IIngredientGroup)p).Id = containerIds[p.GetHashCode()];
                }
            }
        }

        private static void RecordsEquipment(object[] parameters)
        {
            foreach (object p in parameters)
            {
                if (p is IEquipment)
                {
                    if (!equipment.Contains(p))
                    {
                        equipment.Add((IEquipment)p);
                    }
                }
            }
        }

        public static RecipeDirection[] GenerateDirections(RecipeTreeNode rtn, float amount)
        {
            ClearStatics();

            RunRecipe(rtn, amount);

            return directions.ToArray();
        }

        public static IEquipment[] EquipmentNeeded(RecipeTreeNode rtn, float amount)
        {
            ClearStatics();

            RunRecipe(rtn, amount);

            return equipment.OrderBy(e => e.GetName(languageISO)).ToArray();
        }

        public static string[] Nutritions()
        {
            ClearStatics();

            List<string> result = new List<string>();

            return result.ToArray();
        }

        private static void ClearStatics()
        {
            directions.Clear();
            ingredients.Clear();
            seenIngredients.Clear();
            containerIds.Clear();
            equipment.Clear();

            ignoreDirections = false;
        }

        private static void RunRecipe(RecipeTreeNode rtn, float amount)
        {
            if (!rtn.IsImplemented) return;

            currentRecipeUniqieName = rtn.UniqueName;
            languageISO = rtn.LanguageISO;

            if (rtn.SelectEquipment != null)
            {
                stage = RecipeStage.EquipmentSelection;
                EquipmentGroup eg = rtn.SelectEquipment(amount);
                if (rtn.Prepare != null)
                {
                    stage = RecipeStage.Preparation;
                    PreparedIngredients preps = rtn.Prepare(amount, eg);
                    if (rtn.Cook != null)
                    {
                        stage = RecipeStage.Cooking;
                        CookedFoodParts cfp = rtn.Cook(amount, preps, eg);
                        if (rtn.Serve != null)
                        {
                            stage = RecipeStage.Serving;
                            rtn.Serve(amount, cfp, eg);
                        }
                    }
                }
            }

        }
    }
}
