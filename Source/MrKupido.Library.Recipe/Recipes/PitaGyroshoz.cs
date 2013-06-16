using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "pita for gyros")]
    [NameAlias("hun", "pita gyroshoz")]

    public class PitaGyroshoz : RecipeBase
    {
        public PitaGyroshoz(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();
            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            eg.Use<Edeny>(1).Berakni(new Eleszto(2.5f * amount), new Viz(2.0f), new Cukor(1.0f * amount, MeasurementUnit.teaskanal), new So(7.50f * amount));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Edeny>(1));

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(8.0f * amount, MeasurementUnit.kilogramm));
            eg.Use<NagyEdeny>(1).BeonteniC(eg.Use<Edeny>(1));
            eg.Use<NagyEdeny>(1).BeonteniI(new Viz(5.0f * amount));

            eg.Use<NagyEdeny>(1).Lefedni(new Konyharuha());
			eg.Use<NagyEdeny>(1).Varni(new Quantity(1, MeasurementUnit.hour));
            eg.Use<NagyEdeny>(1).FedotLevenni();

			eg.Use<Kez>(1).GolyovaGyurniC(eg.Use<NagyEdeny>(1), 2.0f, eg.Use<LaposTanyer>(1));
			eg.Use<LaposTanyer>(1).Varni(new Quantity(15, MeasurementUnit.minute));

			eg.Use<MelyTanyer>(1).BerakniI(new Liszt(20.0f * amount));
			eg.Use<Kez>(1).MegforgatniC(eg.Use<MelyTanyer>(1), eg.Use<LaposTanyer>(1), eg.Use<LaposTanyer>(2));

			result.Add("pitagolyo", eg.Use<LaposTanyer>(2));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<Suto>(1).Homerseklet(300);
            eg.Use<NyujtoDeszka>(1).NyujtaniC(preps["pitagolyo"], 0.5f);

            eg.Use<Tepsi>(1).Kibelelni(new Sutopapir());
            eg.Use<Tepsi>(1).BerakniC(eg.Use<NyujtoDeszka>(1));

            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
			eg.Use<Suto>(1).Varni(new Quantity(3, MeasurementUnit.minute));
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

            eg.Use<Kez>(1).MegforditaniC(eg.Use<Tepsi>(1));

            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
			eg.Use<Suto>(1).Varni(new Quantity(3, MeasurementUnit.minute));
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

            cfp.Add("pita", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["pita"], eg.Use<LaposTanyer>(3));
            eg.WashUp();
        }
    }
}
