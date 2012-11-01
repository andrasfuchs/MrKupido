using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "pizza (paradicsomos)")]

    public class ParadicsomosAlapuPizza : Pizza
    {
        public ParadicsomosAlapuPizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = Pizza.Prepare(amount, eg);

            IIngredient pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            IIngredient paradicsomosPizzaszosz = new PizzaParadicsomszosz(0.2f);
            Kez kez = eg.Use<Kez>();
            pizzateszta = kez.Raonteni(pizzateszta, paradicsomosPizzaszosz);

            result.Add("pizzateszta", pizzateszta);

            eg.WashUp();
            return result;
        }
    }
}