using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "chicken breast with cream [Fuchs style]")]
    [NameAlias("hun", "tejszínes csirkemell [Fuchs módra]")]

    //TODO: [Recommend(FootRizs, BurgonyaPure)]
    public class TejszinesCsirkemellFuchs : TejszinesCsirkemell
    {
        public TejszinesCsirkemellFuchs(float amount, MeasurementUnit unit = MeasurementUnit.portion)
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

            LaposKisTanyer laposKisTanyer = eg.Use<LaposKisTanyer>();

            Reszelo reszelo = eg.Use<Reszelo>();
            reszelo.LereszelniI(laposKisTanyer, new FustoltSajt(100.0f));

            NagyEdeny edeny = eg.Use<NagyEdeny>();
            edeny.Berakni(new Liszt(250.0f), new So(20.0f), new Tojas(2.0f), laposKisTanyer.Contents);

            Kez kez = eg.Use<Kez>();
            IngredientGroup csirkemell = kez.MegforgatniI(edeny, new Csirkemell(250 * 10));

            JenaiTal jenai = eg.Use<JenaiTal>();
            jenai.Berakni(csirkemell, new FustoltSzalonna(5, MeasurementUnit.piece));
            jenai.BeonteniI(new Tejszin(0.3f));
            jenai.Lefedni(jenai.Fedo);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.BehelyezniC(jenai);
            suto.Varni(30);
            suto.KiemelniC(jenai);
            jenai.FedotLevenni();
            suto.BehelyezniC(jenai);
            suto.Varni(10);
            suto.KiemelniC(jenai);

            cfp.Add("csirkemellek", jenai);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["csirkemellek"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}
