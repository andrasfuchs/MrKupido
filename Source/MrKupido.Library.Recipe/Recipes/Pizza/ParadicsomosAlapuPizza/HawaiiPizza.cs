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
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Kes());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            IIngredient pizzateszta = new ParadicsomosAlapuPizza(amount * 1.0f);
            result.Remove("pizzateszta");

            Kes kes = eg.Use<Kes>();
            IIngredient ananaszdarabok = kes.Feldarabolni(new Ananasz(100.0f), 5.0f);
            IIngredient sonka = kes.Feldarabolni(new Sonka(50.0f), 1.0f);
            IIngredient kukorica = new MorzsoltFottKukorica(50.0f);

            Kez kez = eg.Use<Kez>();
            pizzateszta = kez.Rarakni(pizzateszta, sonka);
            pizzateszta = kez.Rarakni(pizzateszta, kukorica);
            pizzateszta = kez.Rarakni(pizzateszta, ananaszdarabok);
            
            result.Add("pizzateszta", pizzateszta);

            eg.WashUp();
            return result;
        }

        public static new CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();
            cfp.Add("pizzateszta", preps["pizzateszta"]);
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
