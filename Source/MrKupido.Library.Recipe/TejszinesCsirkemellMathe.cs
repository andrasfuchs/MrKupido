using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "tejszínes csirkemell [Máthé módra]")]

    //TODO: [Recommend(FootRizs, BurgonyaPure)]
    [CommercialProductOf("Máthéné Éva")]
    public class TejszinesCsirkemellMathe : TejszinesCsirkemell
    {
        public TejszinesCsirkemellMathe(float amount, MeasurementUnit unit = MeasurementUnit.portion)
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
            result.Tools.Add(new FokhagymaPres());
            result.Tools.Add(new Reszelo());
            
            //? result.Materials.Add(new Alufolia(30.0f, 40.0f));

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            IIngredient fokhagyma = new Fokhagyma(6.0f);
            FokhagymaPres fp = eg.Use<FokhagymaPres>();
            fokhagyma = fp.Preselni(fokhagyma);

            Fakanal fakanal = eg.Use<Fakanal>();
            IIngredient tej = fakanal.Osszekeverni(new Tej(1.0f), fokhagyma);

            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(new Csirkemell(250 * 10));
            edeny.Beonteni(tej);
            edeny.Varni(8 * 60);
            edeny.FolyadekotLeonteni();            
            IIngredient tejesCsirkemell = edeny.Kivenni();

            result.Add("tejescsirkemell", tejesCsirkemell);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Kez kez = eg.Use<Kez>();

            IIngredient tejesCsirkemell = preps["tejescsirkemell"];
            tejesCsirkemell = kez.Raszorni(tejesCsirkemell, new So(10.0f));

            IIngredient alma = new Alma(4.0f);
            alma.ChangeUnitTo(MeasurementUnit.gramm);

            Reszelo reszelo = eg.Use<Reszelo>();
            IIngredient reszeltAlma = reszelo.Lereszelni(alma);

            IIngredient reszeltSajt = reszelo.Lereszelni(new KaravanFustoltSajt(200.0f));

            JenaiTal jenai = eg.Use<JenaiTal>();
            jenai.Berakni(new FustoltSzalonna(5, MeasurementUnit.piece));
            jenai.Berakni(tejesCsirkemell);
            jenai.Berakni(reszeltAlma);
            jenai.Beonteni(new Tejszin(0.3f));
            kez.Raszorni(tejesCsirkemell, reszeltSajt);
            jenai.Berakni(new FustoltSzalonna(5, MeasurementUnit.piece));

            Alufolia alufolia = new Alufolia(29.0f, 1000.0f);
            jenai.Lefedni(alufolia);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.Behelyezni(jenai);
            suto.Varni(60);
            jenai = (JenaiTal)suto.Kiemelni(typeof(JenaiTal));
            jenai.FedotLevenni();
            suto.Behelyezni(jenai);
            suto.Varni(15);
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
