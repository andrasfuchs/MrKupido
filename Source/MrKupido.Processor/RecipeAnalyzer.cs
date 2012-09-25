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

namespace MrKupido.Processor
{
    public class RecipeAnalyzer
    {
        private static List<IngredientBase> ingredients = new List<IngredientBase>();
        private static List<RecipeDirection> directions = new List<RecipeDirection>();
        private static RecipeStage stage = RecipeStage.Unknown;

        public static IIngredient[] GenerateIngredients(RecipeTreeNode rtn, float amount)
        {
            ingredients.Clear();

            RunRecipe(rtn, amount);
            
            return ingredients.ToArray();
        }

        public static object NewIngredient(object returnedObject)
        {
            if (returnedObject is IngredientBase)
            {
                IngredientBase ib = (IngredientBase)returnedObject;

                IngredientBase item = ingredients.FirstOrDefault(i => (i.Name == ib.Name) && (i.Unit == ib.Unit));

                if (item == null) ingredients.Add(ib);
                else item.Add(ib);
            }

            return returnedObject;
        }

        public static void DirectionGenerator(string methodFullName, object result, params object[] parameters)
        {
            directions.Add(new RecipeDirection(methodFullName, parameters, result, stage));
        }

        public static RecipeDirection[] GenerateDirections(RecipeTreeNode rtn, float amount)
        {
            directions.Clear();
            
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
