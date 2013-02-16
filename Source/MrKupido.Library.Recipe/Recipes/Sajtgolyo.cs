using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cheese ball")]
    [NameAlias("hun", "sajtgolyó")]

    public class Sajtgolyo : RecipeBase
    {
        public Sajtgolyo(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Labas());
            result.Containers.Add(new Serpenyo());
            result.Containers.Add(new Edeny());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new MelyTanyer());
            result.Containers.Add(new MelyTanyer());
            result.Containers.Add(new MelyTanyer());
            result.Containers.Add(new LaposKisTanyer());

            result.Devices.Add(new Tuzhely());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Fakanal());
            result.Tools.Add(new Reszelo());
            result.Tools.Add(new KrumpliPucolo());
            result.Tools.Add(new BurgonyaPres());
            result.Tools.Add(new FokhagymaPres());
            result.Tools.Add(new Habvero());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Labas labas = eg.Use<Labas>();
            labas.Berakni(new Burgonya(2.0f), new Viz(0.5f));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Behelyezni(labas);
            tuzhely.Homerseklet(350);
            labas.Varni(20);
            labas = (Labas)tuzhely.Kiemelni(typeof(Labas));
            labas.FolyadekotLeonteni();

            ISingleIngredient burgonya = (ISingleIngredient)labas.Kivenni();
            KrumpliPucolo kp = eg.Use<KrumpliPucolo>();
            kp.Meghamozni(burgonya);
            burgonya.ChangeUnitTo(MeasurementUnit.gramm);

            BurgonyaPres bp = eg.Use<BurgonyaPres>();
            bp.Preselni(burgonya);

            ISingleIngredient fokhagyma = new Fokhagyma(1.0f);
            FokhagymaPres fp = eg.Use<FokhagymaPres>();
            fp.Preselni(fokhagyma);

            Reszelo reszelo = eg.Use<Reszelo>();
            LaposKisTanyer laposKisTanyer = eg.Use<LaposKisTanyer>();

            ISingleIngredient sajt = new Sajt(70.0f);
            reszelo.Lereszelni(laposKisTanyer, sajt);

            IIngredient tojas = new Tojas(1.0f);
            Kez kez = eg.Use<Kez>();
            IIngredient tojasfeherje = kez.Szetvalasztani(tojas)[1];

            Habvero hv = eg.Use<Habvero>();
            tojas = hv.Felverni(tojasfeherje);

            Serpenyo serpenyo = eg.Use<Serpenyo>();
            serpenyo.Berakni(new Vaj(40.0f));
            tuzhely.Behelyezni(serpenyo);
            tuzhely.Homerseklet(200);
            serpenyo.Varni(5);
            serpenyo = (Serpenyo)tuzhely.Kiemelni(typeof(Serpenyo));
            IIngredient vaj = serpenyo.Kivenni();

            Fakanal fakanal = eg.Use<Fakanal>();
            eg.Use<NagyEdeny>(1).Berakni(new Liszt(50.0f), new FeketeBorsOrolt(3.0f), new So(6.0f), vaj, burgonya, tojas, fokhagyma, sajt);
            fakanal.ElkeverniEdenyben(eg.Use<NagyEdeny>(1));

            LaposTanyer laposTanyer = eg.Use<LaposTanyer>();

            IngredientGroup golyok = kez.Kiszaggatni(eg.Use<NagyEdeny>(1).Contents, 30.0f);
            golyok = kez.GolyovaGyurni(golyok);
            laposTanyer.Berakni(golyok);

            result.Add("golyok", laposTanyer);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Serpenyo serpenyo = eg.Use<Serpenyo>();
            serpenyo.Berakni(new NapraforgoOlaj(0.1f));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Behelyezni(serpenyo);
            tuzhely.Homerseklet(350);

            MelyTanyer melyTanyer = eg.Use<MelyTanyer>();
            melyTanyer.Berakni(new Zsemlemorzsa(100.0f));

            Kez kez = eg.Use<Kez>();
            IngredientGroup golyok = preps["golyok"].Contents as IngredientGroup;
            golyok = kez.Megforgatni(melyTanyer, golyok);

            MelyTanyer melyTanyer3 = eg.Use<MelyTanyer>();
            melyTanyer3.Berakni(golyok);

            MelyTanyer melyTanyer2 = eg.Use<MelyTanyer>();
            melyTanyer2.Berakni(serpenyo.KisutniOsszeset(melyTanyer3, 5));

            IIngredient keszgolyok = kez.Lecsepegtetni(golyok);

            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(keszgolyok);

            cfp.Add("osszesgolyo", edeny);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["osszesgolyo"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
