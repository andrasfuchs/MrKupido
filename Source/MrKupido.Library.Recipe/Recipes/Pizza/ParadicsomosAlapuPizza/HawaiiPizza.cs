using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "Hawaii pizza")]
    [NameAlias("hun", "hawaii pizza")]

    public class HawaiiPizza : ParadicsomosAlapuPizza
    {
        public HawaiiPizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = ParadicsomosAlapuPizza.SelectEquipment(amount);

            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Kes());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = ParadicsomosAlapuPizza.Prepare(amount, eg);

            IIngredientContainer pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            Kes kes = eg.Use<Kes>();
            ISingleIngredient ananaszdarabok = new Ananasz(100.0f);
            kes.Feldarabolni(ananaszdarabok, 5.0f);
            
            ISingleIngredient sonka = new Sonka(50.0f);
            kes.Feldarabolni(sonka, 1.0f);
            
            IIngredient kukorica = new MorzsoltFottKukorica(50.0f);

            Kez kez = eg.Use<Kez>();
            kez.Rarakni(pizzateszta, sonka, kukorica, ananaszdarabok);
            
            result.Add("pizzateszta", pizzateszta);

            eg.WashUp();
            return result;
        }

        public static new CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = ParadicsomosAlapuPizza.Cook(amount, preps, eg);
            return cfp;
        }

        public static new void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["pizzateszta"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}
