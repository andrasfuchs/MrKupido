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
            labas.Berakni(new Burgonya(2.0f * amount), new Viz(0.5f * amount));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.RahelyezniC(labas);
            tuzhely.Homerseklet(350);
            labas.Varni(20);
            tuzhely.LeemelniC(labas);
            labas.FolyadekotLeonteni();

            ISingleIngredient burgonya = (ISingleIngredient)labas.Kivenni();
            KrumpliPucolo kp = eg.Use<KrumpliPucolo>();
            kp.MeghamozniI(burgonya);
            burgonya.ChangeUnitTo(MeasurementUnit.gramm);

            BurgonyaPres bp = eg.Use<BurgonyaPres>();
            bp.PreselniI(burgonya);

            ISingleIngredient fokhagyma = new Fokhagyma(1.0f * amount);
            FokhagymaPres fp = eg.Use<FokhagymaPres>();
            fp.PreselniI(fokhagyma);

            Reszelo reszelo = eg.Use<Reszelo>();
            LaposKisTanyer laposKisTanyer = eg.Use<LaposKisTanyer>();

            ISingleIngredient sajt = new Sajt(7.0f * amount);
            reszelo.LereszelniI(laposKisTanyer, sajt);

            IIngredient tojas = new Tojas(1.0f * amount);
            Kez kez = eg.Use<Kez>();
            IIngredient tojasfeherje = kez.SzetvalasztaniI(tojas)[1];

            Habvero hv = eg.Use<Habvero>();
            tojas = hv.FelverniI(tojasfeherje);

            Serpenyo serpenyo = eg.Use<Serpenyo>();
            serpenyo.Berakni(new Vaj(4.0f * amount));
            tuzhely.RahelyezniC(serpenyo);
            tuzhely.Homerseklet(200);
            serpenyo.Varni(5);
            tuzhely.LeemelniC(serpenyo);
            IIngredient vaj = serpenyo.Kivenni();

            Fakanal fakanal = eg.Use<Fakanal>();
            eg.Use<NagyEdeny>(1).Berakni(new Liszt(50.0f * amount), new FeketeBorsOrolt(3.0f * amount), new So(6.0f * amount), vaj, burgonya, tojas, fokhagyma, sajt);
            fakanal.ElkeverniC(eg.Use<NagyEdeny>(1));

            //IngredientGroup golyok = kez.Kiszaggatni(eg.Use<NagyEdeny>(1).Contents, 30.0f);
			kez.GolyovaGyurniC(eg.Use<NagyEdeny>(1), 30.0f, eg.Use<LaposTanyer>(1));

			result.Add("golyok", eg.Use<LaposTanyer>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Serpenyo serpenyo = eg.Use<Serpenyo>();
            serpenyo.Berakni(new NapraforgoOlaj(0.1f));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Homerseklet(350);
            tuzhely.RahelyezniC(serpenyo);

            MelyTanyer melyTanyer = eg.Use<MelyTanyer>();
            melyTanyer.BerakniI(new Zsemlemorzsa(100.0f));

            Kez kez = eg.Use<Kez>();
            //IIngredient golyok = preps["golyok"].Contents;
			kez.MegforgatniC(melyTanyer, preps["golyok"], eg.Use<MelyTanyer>(3));

            MelyTanyer melyTanyer2 = eg.Use<MelyTanyer>();
			melyTanyer2.Berakni(serpenyo.KisutniOsszesetC(eg.Use<MelyTanyer>(3), 5));

            kez.LecsepegtetniC(melyTanyer2);

            cfp.Add("osszesgolyo", melyTanyer2);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["osszesgolyo"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
