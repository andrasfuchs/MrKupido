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

            object eg = InvokePatched(rtn.StandardPatchedInstance, "SelectEquipment", amount);
            object preps = InvokePatched(rtn.StandardPatchedInstance, "Prepare", amount, eg);
            object cfp = InvokePatched(rtn.StandardPatchedInstance, "Cook", amount, preps, eg);
            InvokePatched(rtn.StandardPatchedInstance, "Serve", amount, cfp, eg);

            return ingredients.ToArray();
        }

        public static object InterceptionMethod(object returnedObject)
        {
            if (returnedObject is IngredientBase) ingredients.Add((IngredientBase)returnedObject);

            return returnedObject;
        }

        private object InvokePatched(object patchedRecipe, string methodName, params object[] args)
        {
            return patchedRecipe.GetType().GetMethod(methodName).Invoke(patchedRecipe, args);
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
