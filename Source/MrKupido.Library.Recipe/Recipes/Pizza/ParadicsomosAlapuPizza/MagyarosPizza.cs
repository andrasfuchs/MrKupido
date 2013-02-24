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

            result.Containers.Add(new LaposKisTanyer());
            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Kes());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = ParadicsomosAlapuPizza.Prepare(amount, eg);

            IIngredientContainer pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            Kes kes = eg.Use<Kes>();
            ISingleIngredient szalonna = new FustoltSzalonna(150.0f * amount);
            kes.FeldarabolniI(szalonna, 7.5f);

            ISingleIngredient kolbasz = new Kolbasz(250.0f * amount);
            kes.FelkarikazniI(kolbasz, 2.5f);

            ISingleIngredient hagyma = new Hagyma(5.0f * amount);
            kes.FelkarikazniI(hagyma, 1.5f);

            ISingleIngredient paprika = new Fuszerpaprika(100.0f * amount);
            kes.FelkarikazniI(paprika, 1.5f);

            Reszelo reszelo = eg.Use<Reszelo>();
            LaposKisTanyer laposKisTanyer = eg.Use<LaposKisTanyer>();

            ISingleIngredient sajt = new Sajt(100.0f * amount);
            reszelo.LereszelniI(laposKisTanyer, sajt);

            Kez kez = eg.Use<Kez>();
            kez.Rarakni(pizzateszta, szalonna, kolbasz, hagyma, paprika);
            kez.Raszorni(pizzateszta, sajt);
            
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
            kez.TalalniC(food["pizzateszta"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}
