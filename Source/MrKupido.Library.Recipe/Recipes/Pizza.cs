using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "pizza")]
    [NameAlias("eng", "pizza")]

    [IngredientConsts(Category = ShoppingListCategory.Pizza, IsInline = true)]
    public class Pizza : RecipeBase
    {
        public Pizza(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new NagyEdeny());
            result.Containers.Add(new NagyEdeny());
            result.Containers.Add(new Bogre());
            result.Containers.Add(new Tepsi());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new NyujtoDeszka());

            result.Devices.Add(new Suto());

            result.Tools.Add(new Fakanal());
            result.Tools.Add(new Kez());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Fakanal fakanal = eg.Use<Fakanal>();

            NagyEdeny edeny1 = eg.Use<NagyEdeny>();
            edeny1.Berakni(new Liszt(1000f * amount), new So(6.0f * amount), new Oregano(3.0f * amount), new FeketeBorsOrolt(5.0f * amount));
            fakanal.ElkeverniC(edeny1);

            Bogre bogre = eg.Use<Bogre>();
            bogre.Berakni(new Eleszto(14.0f * amount), new Cukor(1.5f * amount), new Viz(0.6f * amount), new OlivaOlaj(0.05f * amount));
            fakanal.ElkeverniC(bogre);
            

            fakanal.OsszekeverniCC(edeny1, bogre);
            edeny1.Varni(45);

            NyujtoDeszka nyd = eg.Use<NyujtoDeszka>();
            nyd.NyujtaniC(edeny1, 1.0f);

            Tepsi tepsi = eg.Use<Tepsi>();
            tepsi.BerakniC(nyd);

            result.Add("pizzateszta", tepsi);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            IIngredientContainer tepsi = preps["pizzateszta"];

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.BehelyezniC(tepsi);
            suto.Varni(30);
            suto.KiemelniC(tepsi);

            cfp.Add("pizzaalap", tepsi);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["pizzaalap"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}