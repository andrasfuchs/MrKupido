using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Provider;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "Hungarian pizza")]
    [NameAlias("hun", "magyaros pizza")]

    public class MagyarosPizza : ParadicsomosAlapuPizza
    {
        public MagyarosPizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = ParadicsomosAlapuPizza.SelectEquipment(amount);

            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Kes());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = ParadicsomosAlapuPizza.Prepare(amount, eg);

            IIngredient pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            Kes kes = eg.Use<Kes>();
            IIngredient szalonna = kes.Feldarabolni(new FustoltSzalonna(150.0f * amount), 75.0f);
            IIngredient kolbasz = kes.Felkarikazni(new Kolbasz(250.0f * amount), 25.0f);
            IIngredient hagyma = kes.Felkarikazni(new Hagyma(5.0f * amount), 15.0f);
            IIngredient paprika = kes.Felkarikazni(new Fuszerpaprika(100.0f * amount), 15.0f);

            Reszelo reszelo = eg.Use<Reszelo>();
            IIngredient sajt = reszelo.Lereszelni(new Sajt(100.0f * amount));

            Kez kez = eg.Use<Kez>();
            pizzateszta = kez.RarakniMind(pizzateszta, szalonna, kolbasz, hagyma, paprika);
            pizzateszta = kez.Raszorni(pizzateszta, sajt);
            
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
