using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "scone")]
    [NameAlias("hun", "lángos")]

    [IngredientConsts(ManTags = "Cheap")]
    public class Langos : RecipeBase
    {
        public Langos(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(100.0f * amount), new FelfuttatottEleszto(5.0f * amount), new Tejfol(2.0f * amount), new So(15.0f * amount), new Viz(2.0f * amount));

            eg.Use<Kez>(1).OsszegyurniC(eg.Use<NagyEdeny>(1));
            eg.Use<NagyEdeny>(1).Lefedni(eg.Use<NagyEdeny>(1).Fedo);
            eg.Use<NagyEdeny>(1).Varni(new Quantity(30, MeasurementUnit.minute));

            eg.Use<NyujtoDeszka>(1).NyujtaniC(eg.Use<NagyEdeny>(1), 5.0f);
            eg.Use<Szaggato>(1).KiszaggatniC(eg.Use<NyujtoDeszka>(1), 15.0f, 20.0f);

            result.Add("tesztadarabok", eg.Use<NyujtoDeszka>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<Serpenyo>(1).Berakni(new NapraforgoOlaj(5.0f));

            eg.Use<Tuzhely>(1).Homerseklet(350);
            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Serpenyo>(1));

            IIngredientContainer langosok = preps["tesztadarabok"];

            eg.Use<Serpenyo>(1).KisutniOsszesetC(langosok, 2.0f, eg.Use<MelyTanyer>(1));

            cfp.Add("osszeslangos", eg.Use<MelyTanyer>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["osszeslangos"], eg.Use<LaposTanyer>(1));

            eg.WashUp();
        }
    }
}
