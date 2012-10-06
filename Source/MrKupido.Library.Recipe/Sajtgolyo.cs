using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
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
            labas.Berakni(new Burgonya(2.0f));
            labas.Berakni(new Viz(0.5f));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Behelyezni(labas);
            tuzhely.Homerseklet(350);
            labas.Varni(20);
            labas = (Labas)tuzhely.Kiemelni(typeof(Labas));
            labas.FolyadekotLeonteni();

            IIngredient burgonya = labas.Kivenni();
            KrumpliPucolo kp = eg.Use<KrumpliPucolo>();
            burgonya = kp.Meghamozni(burgonya);

            BurgonyaPres bp = eg.Use<BurgonyaPres>();
            burgonya = bp.Preselni(burgonya);

            IIngredient fokhagyma = new Fokhagyma(1.0f);
            FokhagymaPres fp = eg.Use<FokhagymaPres>();
            fokhagyma = fp.Preselni(fokhagyma);

            Reszelo reszelo = eg.Use<Reszelo>();
            IIngredient sajt = reszelo.Lereszelni(new Sajt(70.0f));

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
            IIngredient massza = fakanal.Osszekeverni(new Liszt(50.0f), new FeketeBorsOrolt(3.0f), new So(6.0f), vaj, burgonya, tojas, fokhagyma, sajt);

            IIngredient golyok = kez.Kiszaggatni(massza, 30.0f);
            golyok = kez.GolyovaGyurni(golyok);

            result.Add("golyok", golyok);

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

            Edeny edeny = eg.Use<Edeny>();

            Kez kez = eg.Use<Kez>();
            IIngredient golyok = kez.Megforgatni(preps["golyok"], new Zsemlemorzsa(100.0f));

            bool mindbefert = false;
            do
            {
                mindbefert = serpenyo.Berakni(golyok);
                serpenyo.Varni(5);

                IIngredient keszgolyok = serpenyo.Kivenni();
                keszgolyok = kez.Lecsepegtetni(keszgolyok);

                edeny.Berakni(keszgolyok);
            } while (!mindbefert);
            

            cfp.Add("osszesgolyo", edeny.Contents);

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
