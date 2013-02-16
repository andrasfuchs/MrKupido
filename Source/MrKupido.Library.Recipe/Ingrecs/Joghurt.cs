using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "yogurt")]
    [NameAlias("hun", "joghurt")]

    [IngredientConsts(GrammsPerLiter = 1750, StorageTemperature = 5, IsIngrec = true)]
    public class Joghurt : RecipeBase
    {
        public Joghurt(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Tuzhely());
            result.Devices.Add(new Futotest());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Habvero());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Edeny edeny = eg.Use<Edeny>();
            edeny.Beonteni(new Tej(10.0f * amount));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Homerseklet(110);
            tuzhely.Behelyezni(edeny);
            edeny.Varni(10);
            
            edeny = (Edeny)tuzhely.Kiemelni(typeof(Edeny));
            edeny.Varni(5);

            Habvero habvero = eg.Use<Habvero>();
            edeny.Berakni(new Joghurt(0.1f));
            habvero.Elkeverni((IngredientGroup)edeny.Contents);

            Futotest futotest = eg.Use<Futotest>();
            futotest.Behelyezni(edeny);
            futotest.Homerseklet(40);
            edeny.Varni((int)(1.5 * 24 * 60));
            edeny = (Edeny)futotest.Kiemelni(typeof(Edeny));

            cfp.Add("joghurt", edeny);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["joghurt"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
