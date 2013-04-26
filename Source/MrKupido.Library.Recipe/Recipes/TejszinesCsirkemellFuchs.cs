using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "chicken breast with cream [Fuchs style]")]
    [NameAlias("hun", "tejszínes csirkemell [Fuchs módra]")]

    //TODO: [Recommend(FottRizs, BurgonyaPure)]
    public class TejszinesCsirkemellFuchs : TejszinesCsirkemell
    {
        public TejszinesCsirkemellFuchs(float amount, MeasurementUnit unit = MeasurementUnit.portion)
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
            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

			ISingleIngredient csirkemell = new Csirkemell(10 * amount, MeasurementUnit.piece);
			eg.Use<Kez>(1).MegmosniI(csirkemell);
			eg.Use<NagyEdeny>(1).BerakniI(csirkemell);
			eg.Use<NagyEdeny>(1).Raszorni(new So(2.0f * amount, MeasurementUnit.teaskanal));

			eg.Use<MelyTanyer>(1).BerakniI(new Liszt(10.0f * amount));
			eg.Use<MelyTanyer>(2).BerakniI(new Tojas(1.8f * amount));
			eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposTanyer>(1), new FustoltSajt(10.0f * amount));
			eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposTanyer>(1), new Sajt(10.0f * amount));

			eg.Use<Kez>(1).MegforgatniI(csirkemell, eg.Use<MelyTanyer>(1), eg.Use<MelyTanyer>(2), eg.Use<LaposTanyer>(1));
			eg.Use<Kez>(1).KoretekerniI(new FustoltSzalonna(10 * amount, MeasurementUnit.piece), csirkemell);

			eg.Use<JenaiTal>(1).BerakniI(csirkemell);
			eg.Use<JenaiTal>(1).BeonteniI(new Tejszin(3.0f * amount));
			eg.Use<JenaiTal>(1).Lefedni(eg.Use<JenaiTal>(1).Fedo);

            eg.Use<Suto>(1).Homerseklet(180);
			eg.Use<Suto>(1).BehelyezniC(eg.Use<JenaiTal>(1));
            eg.Use<Suto>(1).Varni(60);
			eg.Use<Suto>(1).KiemelniC(eg.Use<JenaiTal>(1));
			eg.Use<JenaiTal>(1).FedotLevenni();
			eg.Use<Suto>(1).BehelyezniC(eg.Use<JenaiTal>(1));
            eg.Use<Suto>(1).Varni(10);
			eg.Use<Suto>(1).KiemelniC(eg.Use<JenaiTal>(1));

			cfp.Add("csirkemellek", eg.Use<JenaiTal>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["csirkemellek"], eg.Use<LaposTanyer>(2));

            eg.WashUp();
        }
    }
}
