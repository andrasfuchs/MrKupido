using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "tejszínes csirkemell [Fuchs módra]")]

    //TODO: [Recommend(FootRizs, BurgonyaPure)]
    [CommercialProductOf("Fuchsné Walter Magdolna")]
    public class TejszinesCsirkemellFuchs : TejszinesCsirkemell
    {
        public TejszinesCsirkemellFuchs(float amount, MeasurementUnit unit = MeasurementUnit.portion)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.5f));
            result.Containers.Add(new JenaiTal(13.4f, 11.3f));
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Suto());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Fakanal());
            result.Tools.Add(new Reszelo());

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

            Reszelo reszelo = eg.Use<Reszelo>();
            IIngredient reszeltSajt = reszelo.Lereszelni(new KaravanFustoltSajt(100.0f));

            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(new Liszt(250.0f));
            edeny.Berakni(new So(20.0f));
            edeny.Berakni(new Tojas(2.0f));
            edeny.Berakni(reszeltSajt);

            IIngredient csirkemell = new Csirkemell(250 * 10);

            Kez kez = eg.Use<Kez>();
            kez.Megforgatni(csirkemell, edeny.Contents);

            JenaiTal jenai = eg.Use<JenaiTal>();
            jenai.Berakni(csirkemell);
            jenai.Berakni(new FustoltSzalonna(5, MeasurementUnit.piece));
            jenai.Beonteni(new Tejszin(0.3f));
            jenai.Lefedni(jenai.Fedo);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.Behelyezni(jenai);
            suto.Varni(30);
            jenai = (JenaiTal)suto.Kiemelni(typeof(JenaiTal));
            jenai.FedotLevenni();
            suto.Behelyezni(jenai);
            suto.Varni(10);
            jenai = (JenaiTal)suto.Kiemelni(typeof(JenaiTal));

            cfp.Add("csirkemellek", jenai.Contents);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["csirkemellek"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
