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
    [NameAlias("eng", "spicy chicken breast")]
    [NameAlias("hun", "fűszeres csirkemell")]

    public class FuszeresCsirkemell : RecipeBase
    {
        public FuszeresCsirkemell(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            ISingleIngredient csirkemell = new Csirkemell(50.0f * amount);
			eg.Use<Kes>(1).FeldarabolniI(csirkemell, 3.0f);

			eg.Use<MelyTanyer>(1).BerakniI(csirkemell);

			eg.Use<Kez>(1).RaszorniI(eg.Use<MelyTanyer>(1), new So(1.5f * amount, MeasurementUnit.teaskanal));

			eg.Use<Edeny>(1).Berakni(new Liszt(6.0f * amount), new So(2.0f * amount, MeasurementUnit.teaskanal), new Fuszerpaprika(1.0f * amount, MeasurementUnit.teaskanal), new FeketeBorsOrolt(2.0f * amount, MeasurementUnit.teaskanal), new Majoranna(1.0f * amount, MeasurementUnit.teaskanal));

			eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Edeny>(1));
			eg.Use<Kez>(1).MegforgatniI(csirkemell, eg.Use<Edeny>(1));
			eg.Use<LaposTanyer>(2).RarakniI(csirkemell);
            
            ISingleIngredient hagyma = new Hagyma(1.0f * amount, MeasurementUnit.piece);
			eg.Use<Kes>(1).FelkarikazniI(hagyma, 0.5f);
			eg.Use<LaposKisTanyer>(1).RarakniI(hagyma);

			eg.Use<Bogre>(1).Beonteni(new Tejfol(2.0f * amount), new NapraforgoOlaj(1.0f * amount));
			eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Bogre>(1));

            ISingleIngredient sajt = new Sajt(10.0f * amount);

			eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposKisTanyer>(2), sajt);

			result.Add("csirkemell", eg.Use<LaposTanyer>(2));
			result.Add("hagyma", eg.Use<LaposKisTanyer>(1));
			result.Add("tejfol", eg.Use<Bogre>(1));
			result.Add("sajt", eg.Use<LaposKisTanyer>(2));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

			eg.Use<Tepsi>(1).BerakniC(preps["csirkemell"]);

			eg.Use<Kez>(1).Raszorni(eg.Use<Tepsi>(1), new Liszt(3.0f * amount));
			eg.Use<Kez>(1).Rarakni(eg.Use<Tepsi>(1), preps["hagyma"].Contents);
			eg.Use<Kez>(1).Ralocsolni(eg.Use<Tepsi>(1), preps["tejfol"]);
			eg.Use<Kez>(1).Raszorni(eg.Use<Tepsi>(1), preps["sajt"].Contents);
			eg.Use<Tepsi>(1).Lefedni(new Alufolia());

			eg.Use<Suto>(1).Homerseklet(200);
			eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
			eg.Use<Suto>(1).Varni(new Quantity(30, MeasurementUnit.minute));

			eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));
			eg.Use<Tepsi>(1).FedotLevenni();

			eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
			eg.Use<Suto>(1).Varni(new Quantity(10, MeasurementUnit.minute));
			eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

            cfp.Add("csirke", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
			eg.Use<Kez>(1).TalalniC(food["csirke"], eg.Use<LaposTanyer>(3));
            eg.WashUp();
        }
    }
}
