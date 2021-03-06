﻿using MrKupido.Library.Attributes;
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
            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = ParadicsomosAlapuPizza.Prepare(amount, eg);

            IIngredientContainer pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            ISingleIngredient ananaszdarabok = new Ananasz(20.0f * amount);
            eg.Use<Kes>(1).FeldarabolniI(ananaszdarabok, 0.5f);

            ISingleIngredient sonka = new Sonka(10.0f * amount);
            eg.Use<Kes>(1).FeldarabolniI(sonka, 1.0f);

            IIngredient kukorica = new MorzsoltFottKukorica(13.5f * amount);

            ISingleIngredient sajt = new Sajt(15.0f * amount);
            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposKisTanyer>(2), sajt);

            eg.Use<Kez>(1).Rarakni(pizzateszta, sonka, kukorica, ananaszdarabok, sajt);

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
            eg.Use<Kez>(1).TalalniC(food["pizzateszta"], eg.Use<LaposTanyer>(1));
            eg.WashUp();
        }
    }
}
