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
	[NameAlias("eng", "Hortobágy meat pancake")]
	[NameAlias("hun", "hortobágyi húsos palacsinta")]

    public class HortobagyiHusosPalacsinta : RecipeBase
    {
		public HortobagyiHusosPalacsinta(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

			ISingleIngredient sertesComb = new SertesComb(40.0f * amount);
			eg.Use<Kes>(1).FeldarabolniI(sertesComb, 15.0f);

			eg.Use<MelyTanyer>(1).BerakniI(sertesComb);

			ISingleIngredient hagyma = new Hagyma(1.0f * amount);
			eg.Use<Kes>(1).FeldarabolniI(hagyma, 0.5f);

			eg.Use<LaposKisTanyer>(1).RarakniI(hagyma);

			ISingleIngredient zoldpaprika = new ZoldPaprika(1.0f * amount);
			eg.Use<Kes>(1).FelkarikazniI(zoldpaprika, 5.0f);


			ISingleIngredient paradicsom = new Paradicsom(1.0f * amount);
			eg.Use<Kes>(1).MeghamozniI(paradicsom);
			eg.Use<Kes>(1).FeldarabolniI(paradicsom, 5.0f);

			eg.Use<LaposTanyer>(1).Rarakni(zoldpaprika, paradicsom);


			result.Add("sertesComb", eg.Use<MelyTanyer>(1));
			result.Add("hagyma", eg.Use<LaposKisTanyer>(1));
			result.Add("paprikaparadicsom", eg.Use<LaposTanyer>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

			eg.Use<Edeny>(1).BerakniC(preps["hagyma"]);
			eg.Use<Edeny>(1).BerakniI(new NapraforgoOlaj(0.5f));

			eg.Use<Tuzhely>(1).Homerseklet(150);
			eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(1));
			eg.Use<Tuzhely>(1).Varni(new Quantity(5, MeasurementUnit.minute));

			eg.Use<Tuzhely>(1).Homerseklet(200);
			eg.Use<Edeny>(1).BerakniC(preps["sertesComb"]);
			eg.Use<Tuzhely>(1).Varni(new Quantity(5, MeasurementUnit.minute));

			eg.Use<Tuzhely>(1).LeemelniC(eg.Use<Edeny>(1));

			eg.Use<Kez>(1).RaszorniI(eg.Use<Edeny>(1), new Fuszerpaprika(2.0f * amount));
			eg.Use<Edeny>(1).BerakniC(preps["paprikaparadicsom"]);
			eg.Use<Kez>(1).RaszorniI(eg.Use<Edeny>(1), new So(2.0f * amount));
			eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Edeny>(1));

			eg.Use<Tuzhely>(1).Homerseklet(120);
			eg.Use<Edeny>(1).Lefedni(eg.Use<Edeny>(1).Fedo);
			eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(1));
			eg.Use<Tuzhely>(1).Varni(new Quantity(25, MeasurementUnit.minute));


			eg.Use<NagyEdeny>(1).Berakni(new Liszt(15.0f * amount), new Tojas(3.0f * amount), new So(2.0f * amount), new Tej(4.0f * amount), new NapraforgoOlaj(0.5f));
			eg.Use<Fakanal>(1).ElkeverniC(eg.Use<NagyEdeny>(1));
			eg.Use<NagyEdeny>(1).Lefedni(new Konyharuha());
			eg.Use<NagyEdeny>(1).Varni(new Quantity(30, MeasurementUnit.minute));

			IIngredient palacsinta = eg.Use<PalacsintaSuto>(1).KisutniOsszesetC(eg.Use<NagyEdeny>(1), 3);
			eg.Use<LaposTanyer>(1).Rarakni(palacsinta);


			eg.Use<Edeny>(1).FolyadekotAtonteni(eg.Use<Edeny>(2));
			eg.Use<HusDaralo>(1).DaralniC(eg.Use<Edeny>(3), eg.Use<Edeny>(1));

			eg.Use<Edeny>(2).BeonteniI(new Tejfol(3.0f * amount));
			
			eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(2));
			eg.Use<Tuzhely>(1).Homerseklet(150);
			eg.Use<Tuzhely>(1).Varni(new Quantity(5, MeasurementUnit.minute));

			eg.Use<Edeny>(2).Szetvalasztani(eg.Use<Edeny>(2), eg.Use<Edeny>(4), 20.0f);
			eg.Use<Edeny>(2).FolyadekotAtonteni(eg.Use<Edeny>(3));

			eg.Use<Suto>(1).Homerseklet(200);
			eg.Use<Suto>(1).Varni(new Quantity(10, MeasurementUnit.minute));


			eg.Use<Kes>(1).Megkenni(eg.Use<LaposTanyer>(1), eg.Use<Edeny>(3));
			eg.Use<Kez>(1).FelgongyolniC(eg.Use<LaposTanyer>(1));

			eg.Use<Tepsi>(1).BerakniC(eg.Use<LaposTanyer>(1));
			eg.Use<Kez>(1).Ralocsolni(eg.Use<Tepsi>(1), eg.Use<Edeny>(4));

			eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
			eg.Use<Suto>(1).Varni(new Quantity(10, MeasurementUnit.minute));
			eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

			eg.Use<Pohar>(1).Beonteni(new Tejfol(1.0f * amount));
			eg.Use<Kez>(1).Ralocsolni(eg.Use<Tepsi>(1), eg.Use<Pohar>(1));

			cfp.Add("palacsinta", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
			eg.Use<Kez>(1).TalalniC(food["palacsinta"], eg.Use<LaposTanyer>(2));
			// TODO: diszites - petrezselyem
            eg.WashUp();
        }
    }
}
