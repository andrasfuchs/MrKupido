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
            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = Pizza.Prepare(amount, eg);

            IIngredientContainer tepsi = result["pizzateszta"];
            result.Remove("pizzateszta");

            IIngredient paradicsomosPizzaszosz = new PizzaParadicsomszosz(2.0f * amount);
            eg.Use<Kez>(1).RaonteniI(tepsi, paradicsomosPizzaszosz);

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
            eg.Use<Kez>(1).TalalniC(food["pizzateszta"], eg.Use<LaposTanyer>(1));
            eg.WashUp();
        }

    }
}