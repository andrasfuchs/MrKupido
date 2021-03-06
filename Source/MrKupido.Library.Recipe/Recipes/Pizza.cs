﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "pizza tészta")]
    [NameAlias("hun", "pizza", Priority = 200)]
    [NameAlias("eng", "pizza")]

    [IngredientConsts(Category = ShoppingListCategory.Pizza, IsInline = true)]
    public class Pizza : RecipeBase
    {
        public Pizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();
            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(50.0f * amount), new So(2.0f * amount, MeasurementUnit.evokanal), new OreganoOrolt(1.0f * amount, MeasurementUnit.evokanal), new FeketeBorsOrolt(2.0f * amount, MeasurementUnit.evokanal));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<NagyEdeny>(1));

            eg.Use<Kez>(1).Morzsol(new Eleszto(2.5f * amount), eg.Use<Bogre>(1));
            eg.Use<Bogre>(1).Berakni(new Cukor(1.0f * amount, MeasurementUnit.teaskanal), new Viz(3.0f * amount), new OlivaOlaj(3.0f * amount, MeasurementUnit.evokanal));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Bogre>(1));

            eg.Use<Fakanal>(1).OsszekeverniCC(eg.Use<NagyEdeny>(1), eg.Use<Bogre>(1));
            eg.Use<NagyEdeny>(1).Varni(new Quantity(45, MeasurementUnit.minute));

            eg.Use<NyujtoDeszka>(1).NyujtaniC(eg.Use<NagyEdeny>(1), 0.5f);

            eg.Use<Pohar>(1).BeonteniI(new OlivaOlaj(0.5f));
            eg.Use<Ecset>(1).KikenniC(eg.Use<Pohar>(1), eg.Use<Tepsi>(1));
            eg.Use<Tepsi>(1).Raszorni(new Liszt(1.0f));
            eg.Use<Tepsi>(1).BerakniC(eg.Use<NyujtoDeszka>(1));

            result.Add("pizzateszta", eg.Use<Tepsi>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            IIngredientContainer tepsi = preps["pizzateszta"];

            eg.Use<Suto>(1).Homerseklet(200);
            eg.Use<Suto>(1).BehelyezniC(tepsi);
            eg.Use<Suto>(1).Varni(new Quantity(30, MeasurementUnit.minute));
            eg.Use<Suto>(1).KiemelniC(tepsi);

            tepsi.Varni(new Quantity(10, MeasurementUnit.minute));
            //eg.Use<Kes>(1).FelszeletelC(tepsi);

            cfp.Add("pizzaalap", tepsi);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["pizzaalap"], eg.Use<LaposTanyer>(1));
            eg.WashUp();
        }
    }
}