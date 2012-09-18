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
        private IRecipe recipe;
        private float amount;

        private static List<IngredientBase> ingredients = new List<IngredientBase>();

        public RecipeAnalyzer(Type recipe, float amount)
        {
            this.recipe = Activator.CreateInstance(recipe, amount) as IRecipe;
            this.amount = amount;
        }

        public IIngredient[] GenerateIngredients()
        {
            RecipeTreeNode rtn = Cache.Recipe[recipe.GetType().Name];

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

        public static object InterceptionMethod(object returnedObject)
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

        public Instruction[] GenerateIntructions()
        {
            List<Instruction> result = new List<Instruction>();

            return result.ToArray();
        }

        public IEquipment[] EquipmentNeeded()
        {
            List<IEquipment> result = new List<IEquipment>();

            return result.ToArray();
        }

        public string[] Nutritions()
        {
            List<string> result = new List<string>();

            return result.ToArray();
        }
    }
}
