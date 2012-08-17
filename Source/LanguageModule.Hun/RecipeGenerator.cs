using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;

namespace MrKupido.LanguageModule.Hun
{
    public class RecipeAnalyzer
    {
        public RecipeAnalyzer(IRecipe recipe, float pm)
        { }

        public IIngredient[] GenerateIngredients()
        {
            List<IIngredient> result = new List<IIngredient>();

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
