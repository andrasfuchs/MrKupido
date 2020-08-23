using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "baked cauliflower")]
    [NameAlias("hun", "tepsis karfiol")]

    public class TepsisKarfiol : RecipeBase
    {
        public TepsisKarfiol(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            ISingleIngredient karfiol = new Karfiol(2.0f * amount);
            eg.Use<Kez>(1).SzetszedniI(karfiol);

            eg.Use<KisLabas>(1).Berakni(new Vaj(5.0f * amount), new Liszt(2.0f * amount, MeasurementUnit.evokanal));

            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<KisLabas>(1));
            eg.Use<Tuzhely>(1).Homerseklet(80); // kis langon
            eg.Use<KisLabas>(1).Varni(new Quantity(3, MeasurementUnit.minute));

            eg.Use<Labas>(1).BeonteniI(new Tej(2.0f * amount));
            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Labas>(1));
            eg.Use<Tuzhely>(1).Homerseklet(60);

            eg.Use<Labas>(1).BerakniC(eg.Use<KisLabas>(1));
            eg.Use<Fakanal>(1).KevergetniC(eg.Use<Labas>(1), 5);

            ISingleIngredient hagyma = new Ujhagyma(5.0f * amount, MeasurementUnit.piece);
            eg.Use<Kes>(1).FelkarikazniI(hagyma, 1.0f);

            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposTanyer>(1), new FustoltSajt(10.0f * amount));

            eg.Use<Labas>(1).Berakni(new Fuszerpaprika(1.0f * amount, MeasurementUnit.teaskanal), new So(1.0f * amount, MeasurementUnit.teaskanal), new FeherBors(1.0f * amount, MeasurementUnit.mokkaskanal, IngredientState.Orolt), new Borokabogyo(2.0f * amount, MeasurementUnit.csipet), hagyma);
            eg.Use<Labas>(1).BerakniC(eg.Use<LaposTanyer>(1));

            eg.Use<Tuzhely>(1).Homerseklet(30); // minimumra
            eg.Use<Tuzhely>(1).Varni(new Quantity(5, MeasurementUnit.minute));

            eg.Use<Labas>(1).BerakniI(new Tojas(2.0f * amount));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Labas>(1));

            eg.Use<Labas>(1).Varni(new Quantity(2, MeasurementUnit.minute));

            eg.Use<JenaiTal>(1).BerakniI(karfiol);
            eg.Use<JenaiTal>(1).BeonteniC(eg.Use<Labas>(1));
            eg.Use<JenaiTal>(1).Lefedni(new Alufolia());

            eg.Use<Suto>(1).Homerseklet(200);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<JenaiTal>(1));

            eg.Use<Suto>(1).Varni(new Quantity(30, MeasurementUnit.minute));

            eg.Use<Suto>(1).KiemelniC(eg.Use<JenaiTal>(1));
            eg.Use<JenaiTal>(1).FedotLevenni();
            eg.Use<Suto>(1).BehelyezniC(eg.Use<JenaiTal>(1));

            eg.Use<Suto>(1).Varni(new Quantity(5, MeasurementUnit.minute));

            eg.Use<Suto>(1).KiemelniC(eg.Use<JenaiTal>(1));

            cfp.Add("karfiol", eg.Use<JenaiTal>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>(1);
            kez.TalalniC(food["karfiol"], eg.Use<LaposTanyer>(2));
            eg.WashUp();
        }
    }
}
