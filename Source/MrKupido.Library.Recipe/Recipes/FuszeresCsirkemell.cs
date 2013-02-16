using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "spicey chicken breast")]
    [NameAlias("hun", "fűszeres csirkemell")]

    public class FuszeresCsirkemell : RecipeBase
    {
        public FuszeresCsirkemell(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny());
            result.Containers.Add(new Tepsi());
            result.Containers.Add(new Bogre());
            //result.Containers.Add(new LaposTanyer());
            //result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new LaposKisTanyer());
            result.Containers.Add(new LaposKisTanyer());

            result.Devices.Add(new Suto());

            result.Tools.Add(new Kez());
            //result.Tools.Add(new Kes());
            result.Tools.Add(new Fakanal());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Kes knife = eg.Use<Kes>(1);

            ISingleIngredient csirkemell = new Csirkemell(500.0f * amount);
            knife.Feldarabolni(csirkemell, 50.0f);

            MelyTanyer melyTanyer = eg.Use<MelyTanyer>(1);
            melyTanyer.Berakni(csirkemell);

            Kez kez = eg.Use<Kez>();
            kez.Raszorni(melyTanyer, new So(5.0f * amount));

            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(new Liszt(70.0f * amount), new So(5.0f * amount), new Fuszerpaprika(5.0f * amount), new FeketeBors(3.0f * amount), new Majoranna(3.0f * amount));

            LaposTanyer laposTanyer2 = eg.Use<LaposTanyer>(2);
            Fakanal fakanal = eg.Use<Fakanal>();
            fakanal.ElkeverniEdenyben(edeny);
            IngredientGroup prezlisCsirkemell = kez.Megforgatni(edeny, csirkemell);
            laposTanyer2.Berakni(prezlisCsirkemell);            
            
            LaposKisTanyer laposKisTanyer1 = eg.Use<LaposKisTanyer>();
            ISingleIngredient hagyma = new Hagyma(1.0f * amount, MeasurementUnit.piece);
            knife.Felkarikazni(hagyma, 5.0f);
            laposKisTanyer1.Berakni(hagyma);

            Bogre bogre = eg.Use<Bogre>();
            bogre.Beonteni(new Tejfol(0.2f * amount), new NapraforgoOlaj(0.1f * amount));
            fakanal.ElkeverniEdenyben(bogre);

            Reszelo reszelo = eg.Use<Reszelo>();
            ISingleIngredient sajt = new Sajt(100.0f * amount);

            LaposKisTanyer laposKisTanyer2 = eg.Use<LaposKisTanyer>();
            reszelo.Lereszelni(laposKisTanyer2, sajt);

            result.Add("csirkemell", laposTanyer2);
            result.Add("hagyma", laposKisTanyer1);
            result.Add("tejfol", bogre);
            result.Add("sajt", laposKisTanyer2);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Tepsi tepsi = eg.Use<Tepsi>();
            tepsi.Berakni(preps["csirkemell"].Contents);

            Kez kez = eg.Use<Kez>();
            kez.Raszorni(tepsi, new Liszt(10.0f * amount));
            kez.Rarakni(tepsi, preps["hagyma"].Contents);
            kez.Ralocsolni(tepsi, preps["tejfol"].Contents);
            kez.Raszorni(tepsi, preps["sajt"].Contents);
            Alufolia alufolia = new Alufolia(29.0f, 1000.0f);
            tepsi.Lefedni(alufolia);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.Behelyezni(tepsi);
            suto.Varni(30);

            tepsi = (Tepsi)suto.Kiemelni(typeof(Tepsi));
            tepsi.FedotLevenni();

            suto.Behelyezni(tepsi);
            suto.Varni(10);
            tepsi = (Tepsi)suto.Kiemelni(typeof(Tepsi));

            cfp.Add("csirke", tepsi);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["csirke"], eg.Use<LaposTanyer>(3));
            eg.WashUp();
        }
    }
}
