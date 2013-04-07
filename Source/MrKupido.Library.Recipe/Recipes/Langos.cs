using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "scone")]
    [NameAlias("hun", "lángos")]

    public class Langos : RecipeBase
    {
        public Langos(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new NyujtoDeszka());
            result.Containers.Add(new NagyEdeny());
            result.Containers.Add(new Serpenyo());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new MelyTanyer());

            result.Devices.Add(new Suto());
            result.Devices.Add(new Tuzhely());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Szaggato());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            IIngredient felfuttatottEleszto = new FelfuttatottEleszto(5.0f * amount);

            NagyEdeny edeny = eg.Use<NagyEdeny>();
            edeny.Berakni(new Liszt(100.0f * amount), felfuttatottEleszto, new Tejfol(2.0f * amount), new So(15f * amount), new Viz(0.5f * amount));

            Kez kez = eg.Use<Kez>();
            kez.OsszegyurniC(edeny);
            edeny.Lefedni(edeny.Fedo);
            edeny.Varni(30);

            NyujtoDeszka nyd = eg.Use<NyujtoDeszka>();
            nyd.NyujtaniC(edeny, 5.0f);

            Szaggato szaggato = eg.Use<Szaggato>();
            szaggato.KiszaggatniC(nyd, 25, 20);

            result.Add("tesztadarabok", nyd);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Serpenyo serpenyo = eg.Use<Serpenyo>();
            serpenyo.Berakni(new NapraforgoOlaj(0.5f));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Homerseklet(350);
            tuzhely.RahelyezniC(serpenyo);

            IIngredientContainer langosok = preps["tesztadarabok"];

            MelyTanyer melyTanyer = eg.Use<MelyTanyer>();
            IIngredient kisutottLangos = serpenyo.KisutniOsszesetC(langosok, 3.5f);
            melyTanyer.Berakni(kisutottLangos);

            cfp.Add("osszeslangos", melyTanyer);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["osszeslangos"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
