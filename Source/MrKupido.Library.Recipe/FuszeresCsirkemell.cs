using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "fűszeres csirkemell")]

    public class FuszeresCsirkemell : RecipeBase
    {
        public FuszeresCsirkemell(float amount)
            : base(amount)
        {
        }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Tepsi());
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Suto(38, 40, 4));

            result.Tools.Add(new Kez());
            result.Tools.Add(new Kes());
            result.Tools.Add(new Fakanal());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static object InterceptionMethod(object returnedObject)
        {
            return returnedObject;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            object l1 = new Liszt(10.0f * amount);
            object l2 = InterceptionMethod(new Liszt(20.0f * amount));

            PreparedIngredients result = new PreparedIngredients();

            Kes knife = eg.Use<Kes>();
            IIngredient csirkemell = knife.Feldarabolni(new Csirkemell(500.0f * amount), 50.0f);
            Kez kez = eg.Use<Kez>();
            csirkemell = kez.Raszorni(csirkemell, new So(5.0f * amount));

            Fakanal fakanal = eg.Use<Fakanal>();
            IngredientGroup fuszeresliszt = fakanal.Osszekeverni(new Liszt(70.0f * amount), new So(5.0f * amount), new Fuszerpaprika(5.0f * amount), new FeketeBors(3.0f * amount), new Majoranna(3.0f * amount));
            csirkemell = kez.Megforgatni(csirkemell, fuszeresliszt);

            IngredientGroup hagyma = knife.Felkarikazni(new Hagyma(1.0f * amount, MeasurementUnit.piece), 5.0f);
            IngredientGroup tejfol = fakanal.Osszekeverni(new NapraforgoOlaj(0.1f * amount), new Tejfol(0.2f * amount));

            Reszelo reszelo = eg.Use<Reszelo>();
            IngredientGroup sajt = reszelo.Lereszelni(new Sajt(100.0f * amount));

            result.Add("csirkemell", csirkemell);
            result.Add("hagyma", hagyma);
            result.Add("tejfol", tejfol);
            result.Add("sajt", sajt);

            eg.WashUp();
            return result;
        }

        public static new CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Tepsi tepsi = eg.Use<Tepsi>();
            tepsi.Berakni(preps["csirkemell"]);

            Kez kez = eg.Use<Kez>();
            tepsi.Contents = kez.Raszorni(tepsi.Contents, new Liszt(10.0f * amount));
            tepsi.Contents = kez.Rarakni(tepsi.Contents, preps["hagyma"]);
            tepsi.Contents = kez.Ralocsolni(tepsi.Contents, preps["tejfol"]);
            tepsi.Contents = kez.Raszorni(tepsi.Contents, preps["sajt"]);
            Alufolia alufolia = new Alufolia(29.0f, 1000.0f);
            tepsi.Lefedni(alufolia);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.Behelyezni(tepsi);
            suto.Varni(30);

            tepsi = suto.Kiemelni<Tepsi>();
            tepsi.FedotLevenni();

            suto.Behelyezni(tepsi);
            suto.Varni(10);
            tepsi = suto.Kiemelni<Tepsi>();

            cfp.Add("csirke", tepsi.Contents);

            eg.WashUp();
            return cfp;
        }

        public override void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["csirke"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}
