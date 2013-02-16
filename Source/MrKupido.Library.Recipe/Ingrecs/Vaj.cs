using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "vaj")]
    [NameAlias("eng", "butter")]

    [IngredientConsts(IsIngrec = true)]
    public class Vaj : RecipeBase
    {
        public Vaj(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new LaposTanyer());

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
            edeny.Beonteni(new Tejszin(10.0f * amount));

            Habvero habvero = eg.Use<Habvero>();
            habvero.Felverni(edeny.Contents);

            cfp.Add("vaj", edeny);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["vaj"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
