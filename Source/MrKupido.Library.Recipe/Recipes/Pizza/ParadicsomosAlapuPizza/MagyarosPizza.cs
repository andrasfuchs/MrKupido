using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "magyaros pizza")]

    public class MagyarosPizza : ParadicsomosAlapuPizza
    {
        public MagyarosPizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Kes());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            IIngredient pizzateszta = new ParadicsomosAlapuPizza(amount * 1.0f);
            result.Remove("pizzateszta");

            Kes kes = eg.Use<Kes>();
            IIngredient szalonna = kes.Feldarabolni(new FustoltSzalonna(50.0f), 1.0f);
            IIngredient kolbasz = kes.Felkarikazni(new Kolbasz(50.0f), 1.0f);
            IIngredient hagyma = kes.Felkarikazni(new Hagyma(35.0f), 4.0f);
            IIngredient paprika = kes.Felkarikazni(new Fuszerpaprika(30.0f), 3.0f);

            Reszelo reszelo = eg.Use<Reszelo>();
            IIngredient sajt = reszelo.Lereszelni(new Sajt(100.0f));

            Kez kez = eg.Use<Kez>();
            pizzateszta = kez.Rarakni(pizzateszta, szalonna);
            pizzateszta = kez.Rarakni(pizzateszta, kolbasz);
            pizzateszta = kez.Rarakni(pizzateszta, hagyma);
            pizzateszta = kez.Rarakni(pizzateszta, paprika);
            pizzateszta = kez.Raszorni(pizzateszta, sajt);
            
            result.Add("pizzateszta", pizzateszta);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();
            cfp.Add("pizzateszta", preps["pizzateszta"]);
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["pizzateszta"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}
