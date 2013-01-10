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

            result.Containers.Add(new Edeny(1.5f));
            result.Containers.Add(new Edeny(1.5f));
            result.Containers.Add(new Bogre());
            result.Containers.Add(new Tepsi());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new NyujtoDeszka());

            result.Devices.Add(new Suto(38, 40, 4));

            result.Tools.Add(new Fakanal());
            result.Tools.Add(new Kez());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Fakanal fakanal = eg.Use<Fakanal>();

            Edeny edeny1 = eg.Use<Edeny>();
            edeny1.BerakniMind(new Liszt(1000f * amount), new So(6.0f * amount), new Oregano(3.0f * amount), new FeketeBorsOrolt(5.0f * amount));
            fakanal.Elkeverni((IngredientGroup)edeny1.Contents);

            Bogre bogre = eg.Use<Bogre>();
            bogre.BerakniMind(new Eleszto(14.0f * amount), new Cukor(1.5f * amount), new Viz(0.6f * amount), new OlivaOlaj(0.05f * amount));
            fakanal.Elkeverni((IngredientGroup)bogre.Contents);
            

            IIngredient pizzateszta = fakanal.Osszekeverni(edeny1.Contents, bogre.Contents);
            
            Edeny edeny2 = eg.Use<Edeny>();
            edeny2.Berakni(pizzateszta);
            edeny2.Varni(45);
            pizzateszta = edeny2.Kivenni();

            NyujtoDeszka nyd = eg.Use<NyujtoDeszka>();
            pizzateszta = nyd.Nyujtani(pizzateszta, 1);

            result.Add("pizzateszta", pizzateszta);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Tepsi tepsi = eg.Use<Tepsi>();
            tepsi.Berakni(preps["pizzateszta"]);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.Behelyezni(tepsi);
            suto.Varni(30);
            tepsi = (Tepsi)suto.Kiemelni(typeof(Tepsi));

            cfp.Add("pizzaalap", tepsi.Contents);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["pizzaalap"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}