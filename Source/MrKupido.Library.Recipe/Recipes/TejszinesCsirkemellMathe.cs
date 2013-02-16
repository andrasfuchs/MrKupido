using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "chicken breast with cream [Máthé style]")]
    [NameAlias("hun", "tejszínes csirkemell [Máthé módra]")]

    //TODO: [Recommend(FootRizs, BurgonyaPure)]
    public class TejszinesCsirkemellMathe : TejszinesCsirkemell
    {
        public TejszinesCsirkemellMathe(float amount, MeasurementUnit unit = MeasurementUnit.portion)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new NagyEdeny());
            result.Containers.Add(new JenaiTal());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new LaposKisTanyer());
            result.Containers.Add(new LaposKisTanyer());

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

            NagyEdeny edeny = eg.Use<NagyEdeny>();

            ISingleIngredient fokhagyma = new Fokhagyma(6.0f);
            FokhagymaPres fp = eg.Use<FokhagymaPres>();
            fp.Preselni(fokhagyma);
            edeny.Berakni(fokhagyma, new Tej(1.0f));

            Fakanal fakanal = eg.Use<Fakanal>();
            fakanal.ElkeverniEdenyben(edeny);

            edeny.Berakni(new Csirkemell(250 * 10));
            edeny.Varni(8 * 60);
            edeny.FolyadekotLeonteni();            

            result.Add("tejescsirkemell", edeny);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Kez kez = eg.Use<Kez>();

            IIngredientContainer tejesCsirkemell = preps["tejescsirkemell"];
            kez.Raszorni(tejesCsirkemell, new So(10.0f));

            ISingleIngredient alma = new Alma(4.0f);
            alma.ChangeUnitTo(MeasurementUnit.gramm);

            LaposKisTanyer laposKisTanyer1 = eg.Use<LaposKisTanyer>();
            Reszelo reszelo = eg.Use<Reszelo>();
            reszelo.Lereszelni(laposKisTanyer1, alma);

            LaposKisTanyer laposKisTanyer2 = eg.Use<LaposKisTanyer>();
            ISingleIngredient sajt = new FustoltSajt(200.0f);
            reszelo.Lereszelni(laposKisTanyer2, sajt);

            JenaiTal jenai = eg.Use<JenaiTal>();
            jenai.Berakni(new FustoltSzalonna(5, MeasurementUnit.piece), tejesCsirkemell.Contents, alma);
            jenai.Beonteni(new Tejszin(0.3f));
            kez.Raszorni(tejesCsirkemell, sajt);
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

            cfp.Add("csirkemellek", jenai);

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
