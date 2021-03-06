﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "apple-cinnamon cookie")]
    [NameAlias("hun", "almás-fahéjas süti")]

    [IngredientConsts(ManTags = "Cake")]
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

            ISingleIngredient alma = new Alma(2.5f * amount);
            eg.Use<KrumpliPucolo>(1).MeghamozniI(alma);
            eg.Use<Kes>(1).FeldarabolniI(alma, 4.0f);
            eg.Use<NagyEdeny>(1).BerakniI(alma);

            eg.Use<Kez>(1).MeglocsolniI(eg.Use<NagyEdeny>(1), new CitromLe(0.5f * amount));

            ISingleIngredient tojas = new Tojas(3.0f * amount);
            tojas.ChangeUnitTo(MeasurementUnit.gramm);

            eg.Use<Edeny>(1).Berakni(tojas, new Cukor(3.0f * amount));
            eg.Use<Habvero>(1).KikeverniC(eg.Use<Edeny>(1));

            ISingleIngredient citromhej = new CitromHej(2.0f * amount, MeasurementUnit.piece);
            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposKisTanyer>(1), citromhej);
            eg.Use<Edeny>(1).Berakni(new NapraforgoOlaj(2.0f * amount), citromhej);

            eg.Use<Edeny>(1).Berakni(new Liszt(40.0f * amount), new Fahej(4.5f * amount, MeasurementUnit.teaskanal), new Szodabikarbona(2.0f * amount, MeasurementUnit.teaskanal));
            eg.Use<NagyEdeny>(1).BeonteniC(eg.Use<Edeny>(1));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<NagyEdeny>(1));

            ISingleIngredient dio = new Diobel(12.0f * amount, MeasurementUnit.dekagramm);
            eg.Use<Daralo>(1).DaralniI(eg.Use<LaposKisTanyer>(2), dio);

            result.Add("alma", eg.Use<NagyEdeny>(1));
            result.Add("dio", eg.Use<LaposKisTanyer>(2));

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
            eg.Use<Suto>(1).Varni(new Quantity(40, MeasurementUnit.minute));

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
