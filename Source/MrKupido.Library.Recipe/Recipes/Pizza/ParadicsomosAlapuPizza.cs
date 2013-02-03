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
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            IIngredient pizzateszta = new Pizza(amount * 1.0f);

            IIngredient paradicsomosPizzaszosz = new PizzaParadicsomszosz(0.2f);
            Kez kez = eg.Use<Kez>();
            pizzateszta = kez.Raonteni(pizzateszta, paradicsomosPizzaszosz);

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