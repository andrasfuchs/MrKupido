using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment.Tools;
using MrKupido.Library.Equipment.Containers;
using MrKupido.Library.Equipment.Devices;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "hawaii pizza")]

    public class HawaiiPizza : ParadicsomosAlapuPizza
    {
        public HawaiiPizza(float amount)
            : base(amount)
        { }

        public override EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = base.SelectEquipment(amount);

            result.Tools.Add(new Kes());

            return result;
        }

        public override PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = base.Prepare(amount, eg);

            IIngredient pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            Kes kes = eg.Use<Kes>();
            IIngredient ananaszdarabok = kes.Feldarabolni(new Ananasz(100.0f), 5.0f);
            IIngredient sonka = kes.Feldarabolni(new Sonka(50.0f), 1.0f);
            IIngredient kukorica = new MorzsoltFottKukorica(50.0f);

            pizzateszta = pizzateszta.Rarakni(sonka);
            pizzateszta = pizzateszta.Rarakni(kukorica);
            pizzateszta = pizzateszta.Rarakni(ananaszdarabok);
            
            result.Add("pizzateszta", pizzateszta);

            return result;
        }
    }
}
