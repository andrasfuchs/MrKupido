using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "pizza paradicsomos alappal")]

    public class ParadicsomosAlapuPizza : Pizza
    {
        public ParadicsomosAlapuPizza(float amount)
            : base(amount)
        { }

        public override PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = base.Prepare(amount, eg);

            IIngredient pizzateszta = result["pizzateszta"];
            result.Remove("pizzateszta");

            IIngredient paradicsomosPizzaszosz = new PizzaParadicsomszosz(0.2f);
            pizzateszta = pizzateszta.Rarakni(paradicsomosPizzaszosz);

            result.Add("pizzateszta", pizzateszta);

            return result;
        }
    }
}