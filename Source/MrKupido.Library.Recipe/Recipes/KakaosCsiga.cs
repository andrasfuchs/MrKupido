using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cocoa spiral")]
    [NameAlias("hun", "kakaós csiga")]

    [IngredientConsts(ManTags = "Cake", PieceCountEstimation = 28, GrammsPerPiece = 34)]
    public class KakaosCsiga : RecipeBase
    {
        public KakaosCsiga(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(40.0f * amount), new TojasSargaja(2.0f * amount), new FelfuttatottEleszto(2.5f * amount), new Vaj(4.0f * amount), new Cukor(3.0f * amount), new So(2.0f * amount));
            eg.Use<Kez>(1).OsszegyurniC(eg.Use<NagyEdeny>(1));

            eg.Use<Futotest>(1).RahelyezniC(eg.Use<NagyEdeny>(1));
            eg.Use<Futotest>(1).Varni(new Quantity(45, MeasurementUnit.minute));
            eg.Use<Futotest>(1).LeemelniC(eg.Use<NagyEdeny>(1));

            result.Add("csigateszta", eg.Use<NagyEdeny>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<NyujtoDeszka>(1).NyujtaniC(preps["csigateszta"], 1.50f);

            ISingleIngredient kakao = new Kakaopor(5.0f * amount, MeasurementUnit.teaskanal);
            kakao.ChangeUnitTo(MeasurementUnit.gramm);

            eg.Use<Edeny>(1).Berakni(kakao, new PorCukor(14.0f * amount), new VaniliasCukor(20.0f * amount), new Vaj(10.0f * amount));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Edeny>(1));

            eg.Use<Ecset>(1).MegkenniC(eg.Use<Edeny>(1), eg.Use<NyujtoDeszka>(1));
            eg.Use<Kez>(1).FelgongyolniC(eg.Use<NyujtoDeszka>(1));

            eg.Use<Kes>(1).FeldarabolniC(eg.Use<NyujtoDeszka>(1), 10.0f);


            eg.Use<Tepsi>(1).Kibelelni(new Sutopapir());
            eg.Use<Tepsi>(1).BerakniC(eg.Use<NyujtoDeszka>(1));
            eg.Use<Ecset>(1).MegkenniI(new TojasSargaja(2.0f * amount), eg.Use<Tepsi>(1));
            eg.Use<Tepsi>(1).Lefedni(new Konyharuha());
            eg.Use<Tepsi>(1).Varni(new Quantity(30, MeasurementUnit.minute));

            eg.Use<Suto>(1).Homerseklet(180);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(new Quantity(20, MeasurementUnit.minute));
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));


            eg.Use<Edeny>(2).BeonteniI(new Tejszin(2.0f * amount));

            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(2));
            eg.Use<Tuzhely>(1).Homerseklet(50);
            eg.Use<Tuzhely>(1).Varni(new Quantity(3, MeasurementUnit.minute));
            eg.Use<Tuzhely>(1).LeemelniC(eg.Use<Edeny>(2));

            eg.Use<Kez>(1).RaonteniC(eg.Use<Tepsi>(1), eg.Use<Edeny>(2));
            eg.Use<Suto>(1).Homerseklet(150);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(new Quantity(10, MeasurementUnit.minute));
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));


            cfp.Add("csiga", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["csiga"], eg.Use<LaposTanyer>(1));
            eg.WashUp();
        }
    }
}
