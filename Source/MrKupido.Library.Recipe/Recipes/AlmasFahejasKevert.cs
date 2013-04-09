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
    [NameAlias("eng", "apple-cinemon cookie")]
    [NameAlias("hun", "almás-fahéjas süti")]

    public class AlmasFahejasKevert : RecipeBase
    {
        public AlmasFahejasKevert(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            ISingleIngredient alma = new Alma(5.0f * amount);
            eg.Use<KrumpliPucolo>(1).MeghamozniI(alma);
            eg.Use<Kes>(1).FeldarabolniI(alma, 2.0f);
            eg.Use<Edeny>(2).Berakni(alma);

            eg.Use<Kez>(1).MeglocsolniI(eg.Use<Edeny>(2), new CitromLe(1.0f * amount));

			ISingleIngredient tojas = new Tojas(3.0f * amount);
			tojas.ChangeUnitTo(MeasurementUnit.gramm);

            eg.Use<Edeny>(1).Berakni(tojas, new Cukor(1.0f * amount));
            eg.Use<Habvero>(1).KikeverniC(eg.Use<Edeny>(1));

            ISingleIngredient citromhej = new CitromHej(1.0f * amount);
            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposKisTanyer>(1), citromhej);
            eg.Use<Edeny>(1).Berakni(new NapraforgoOlaj(2.0f * amount), citromhej);

            eg.Use<Edeny>(2).Berakni(new Liszt(40.0f * amount), new Fahej(15.0f * amount), new Szodabikarbona(3.0f * amount));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Edeny>(2));

            ISingleIngredient dio = new Diobel(150.0f * amount, MeasurementUnit.gramm);
            eg.Use<Daralo>(1).DaralniI(eg.Use<LaposKisTanyer>(1), dio);

            result.Add("alma", eg.Use<Edeny>(2));
            result.Add("dio", eg.Use<LaposKisTanyer>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            IIngredientContainer alma = preps["alma"];
            IIngredientContainer dio = preps["dio"];

            eg.Use<Tepsi>(1).Kibelelni(new Sutopapir());
            eg.Use<Tepsi>(1).BerakniC(alma);
            eg.Use<Kez>(1).RaszorniC(eg.Use<Tepsi>(1), dio);

            eg.Use<Suto>(1).Homerseklet(190);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(40);

            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));
            eg.Use<Kez>(1).RaszorniI(eg.Use<Tepsi>(1), new PorCukor(2.0f * amount));

            cfp.Add("alma", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>(1);
            kez.TalalniC(food["alma"], eg.Use<LaposTanyer>(2));
            eg.WashUp();
        }
    }
}
