using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using Mono.Cecil;

namespace MrKupido.Processor
{
    public class RecipeAnalyzer
    {
        private IRecipe recipe;
        private float amount;

        public RecipeAnalyzer(Type recipe, float amount)
        {
            this.recipe = Activator.CreateInstance(recipe, amount) as IRecipe;
            this.amount = amount;
        }

        public IIngredient[] GenerateIngredients()
        {
            List<IIngredient> result = new List<IIngredient>();

            EquipmentGroup eg = recipe.SelectEquipment(amount);
            PreparedIngredients preps = recipe.Prepare(amount, eg);
            CookedFoodParts cfp = recipe.Cook(amount, preps, eg);
            recipe.Serve(amount, cfp, eg);


            AssemblyDefinition ad = AssemblyDefinition.ReadAssembly(recipe.GetType().Assembly.Location);

            return result.ToArray();
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
