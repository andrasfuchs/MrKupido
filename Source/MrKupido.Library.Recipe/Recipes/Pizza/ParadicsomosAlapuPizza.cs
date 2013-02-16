using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "tomato-based pizza")]
    [NameAlias("hun", "paradicsomos alapú pizza")]

    [IngredientConsts(IsInline = true)]
    public class ParadicsomosAlapuPizza : Pizza
    {
        public ParadicsomosAlapuPizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = Pizza.SelectEquipment(amount);

            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = Pizza.Prepare(amount, eg);

            IIngredientContainer tepsi = result["pizzateszta"];
            result.Remove("pizzateszta");

            IIngredient paradicsomosPizzaszosz = new PizzaParadicsomszosz(0.2f);
            Kez kez = eg.Use<Kez>();
            kez.Raonteni(tepsi, paradicsomosPizzaszosz);

            result.Add("pizzateszta", tepsi);

            eg.WashUp();
            return result;
        }

        public static new CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = Pizza.Cook(amount, preps, eg);
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