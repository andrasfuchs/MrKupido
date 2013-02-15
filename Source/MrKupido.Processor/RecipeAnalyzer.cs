using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using Mono.Cecil;
using System.Reflection.Emit;
using System.Reflection;
using MrKupido.Processor.Model;
using System.Diagnostics;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Recipe;

namespace MrKupido.Processor
{
    public class RecipeAnalyzer
    {
        private static List<IngredientBase> ingredients = new List<IngredientBase>();
        private static List<string> seenIngredients = new List<string>();
        private static List<RecipeDirection> directions = new List<RecipeDirection>();
        private static RecipeStage stage = RecipeStage.Unknown;        
        private static int IdCounter;
        private static List<object> tempParameters = new List<object>();
        private static bool ignoreDirections = false;
        private static string languageISO = "";

        public static IIngredient[] GenerateIngredients(RecipeTreeNode rtn, float amount)
        {
            ingredients.Clear();
            seenIngredients.Clear();

            ignoreDirections = true;
            
            RunRecipe(rtn, amount);

            return ingredients.ToArray();
        }

        public static object NewIngredient(object returnedObject)
        {
            RecipeBase rb = null;
            RecipeTreeNode rtn = null;

            if (returnedObject is RecipeBase)
            {
                rb = ((RecipeBase)returnedObject);
                string rbTypeName = rb.GetType().Name;
                rtn = Cache.Recipe.All.FirstOrDefault(tn => tn.RecipeType.Name == rbTypeName);
            }

            if (returnedObject is IngredientBase)
            {
                IngredientBase ib = (IngredientBase)((IngredientBase)returnedObject).Clone();

                IngredientBase item = ingredients.FirstOrDefault(i => (i.Name == ib.Name) && (i.Unit == ib.Unit));

                if (item == null)
                {
                    if ((rtn == null) || (!rtn.IsInline))
                    {
                        ingredients.Add(ib);
                    }
                } else item.Add(ib);
            }

            if (returnedObject is RecipeBase)
            {
                // if the recipe is inline then we need to run it (without the serving part)
                if ((rtn != null) && (rtn.IsInline))
                {
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
                }
            }

            return returnedObject;
        }

        public static void DirectionGeneratorAfter(string assemblyFullName, string methodFullName, object[] parameters, object result)
        {
            if (ignoreDirections) return;

            directions.Add(new RecipeDirection(languageISO, ref IdCounter, assemblyFullName, methodFullName, tempParameters.ToArray(), result, stage, 1, seenIngredients));
        }

        public static void DirectionGeneratorBefore(string assemblyFullName, string methodFullName, object[] parameters)
        {
            if (ignoreDirections) return;

            tempParameters.Clear();

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

        public static RecipeDirection[] GenerateDirections(RecipeTreeNode rtn, float amount)
        {
            IdCounter = 0;

            directions.Clear();
            seenIngredients.Clear();

            ignoreDirections = false;
            RunRecipe(rtn, amount);

            return directions.ToArray();
        }

        public static IEquipment[] EquipmentNeeded()
        {
            List<IEquipment> result = new List<IEquipment>();

            return result.ToArray();
        }

        public static string[] Nutritions()
        {
            List<string> result = new List<string>();

            return result.ToArray();
        }

        private static void RunRecipe(RecipeTreeNode rtn, float amount)
        {
            if (!rtn.IsImplemented) return;

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
