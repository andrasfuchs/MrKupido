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

        public static IIngredient[] GenerateIngredients(RecipeTreeNode rtn, float amount)
        {
            ingredients.Clear();

            if (rtn.SelectEquipment != null)
            {
                EquipmentGroup eg = rtn.SelectEquipment(amount);
                if (rtn.Prepare != null)
                {
                    PreparedIngredients preps = rtn.Prepare(amount, eg);
                    if (rtn.Cook != null)
                    {
                        CookedFoodParts cfp = rtn.Cook(amount, preps, eg);
                        if (rtn.Serve != null) rtn.Serve(amount, cfp, eg);
                    }
                }
            }
            
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
            return;
        }

        public Instruction[] GenerateIntructions()
        {
            List<Instruction> result = new List<Instruction>();

            return result.ToArray();
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
    }
}
