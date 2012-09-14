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
        public MagyarosPizza(float amount)
            : base(amount)
        { }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = ParadicsomosAlapuPizza.SelectEquipment(amount);

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

            return result;
        }
    }
}
