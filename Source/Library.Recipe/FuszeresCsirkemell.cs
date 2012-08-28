using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment.Containers;
using MrKupido.Library.Equipment.Materials;
using MrKupido.Library.Equipment.Devices;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment.Tools;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "fűszeres csirkemell")]

    class FuszeresCsirkemell : RecipeBase
    {
        public FuszeresCsirkemell(float amount)
            : base(amount)
        {
        }

        public override EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Tepsi());
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Suto(38, 40, 4));

            result.Tools.Add(new Kes());

            return result;
        }

        public override PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Kes knife = eg.Use<Kes>();

            IIngredient csirkemell = knife.Feldarabolni(new Csirkemell(500.0f * amount), 50.0f);
            csirkemell.Raszorni(new So(5.0f * amount));

            Fakanal fakanal = eg.Use<Fakanal>();
            IngredientGroup fuszeresliszt = fakanal.Osszekeverni(new Liszt(70.0f * amount), new So(5.0f * amount), new Fuszerpaprika(5.0f * amount), new FeketeBors(3.0f * amount), new Majoranna(3.0f * amount));
            csirkemell = csirkemell.Megforgatni(fuszeresliszt);

            IngredientGroup hagyma = knife.Felkarikazni(new Hagyma(35.0f * amount), 5.0f);
            IngredientGroup tejfol = fakanal.Osszekeverni(new NapraforgoOlaj(0.1f * amount), new Tejfol(0.2f * amount));

            Reszelo reszelo = eg.Use<Reszelo>();
            IngredientGroup sajt = reszelo.Lereszelni(new Sajt(100.0f * amount));

            result.Add("csirkemell", csirkemell);
            result.Add("hagyma", hagyma);
            result.Add("tejfol", tejfol);
            result.Add("sajt", sajt);

            return result;
        }

        public override CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Tepsi tepsi = eg.Use<Tepsi>();
            tepsi.Berakni(preps["csirkemell"]);

            tepsi.Contents.Raszorni(new Liszt(10.0f * amount));
            tepsi.Contents.Rarakni(preps["hagyma"]);
            tepsi.Contents.Ralocsolni(preps["tejfol"]);
            tepsi.Contents.Raszorni(preps["sajt"]);
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

            return cfp;
        }

        public override void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            food["csirke"].Talalni(eg.Use<LaposTanyer>());
        }
    }
}
