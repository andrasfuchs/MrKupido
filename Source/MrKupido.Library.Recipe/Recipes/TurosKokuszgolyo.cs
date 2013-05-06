using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cottage cheese coconut ball")]
    [NameAlias("hun", "túrós kókuszgolyó")]

	[IngredientConsts(ManTags = "Easy")]
    public class TurosKokuszgolyo : RecipeBase
    {
        public TurosKokuszgolyo(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new MelyTanyer());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

			eg.Use<Reszelo>(1).LereszelniI(eg.Use<Edeny>(1), new CitromHej(0.5f * amount, MeasurementUnit.piece));
			eg.Use<Edeny>(1).Berakni(new Turo(250.0f * amount), new PorCukor(15.0f * amount), new KokuszReszelek(12.5f * amount), new VaniliasCukor(10.0f * amount));

			eg.Use<Kez>(1).OsszegyurniC(eg.Use<Edeny>(1));
			eg.Use<Kez>(1).GolyovaGyurniC(eg.Use<Edeny>(1), 3.0f, eg.Use<LaposKisTanyer>(1));

			eg.Use<MelyTanyer>(1).BerakniI(new KokuszReszelek(1.5f * amount));
			eg.Use<Kez>(1).MegforgatniC(eg.Use<MelyTanyer>(1), eg.Use<LaposKisTanyer>(1), eg.Use<LaposTanyer>(1));

			result.Add("golyok", eg.Use<LaposTanyer>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

			cfp.Add("golyok", preps["golyok"]);
             
            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["golyok"], eg.Use<LaposKisTanyer>(1));
            eg.WashUp();
        }
    }
}
