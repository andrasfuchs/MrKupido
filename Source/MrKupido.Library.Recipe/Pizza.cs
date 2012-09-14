using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "pizza")]
    [NameAlias("eng", "pizza")]

    public class Pizza : RecipeBase
    {
        public Pizza(float amount)
            : base(amount)
        { }

        public static new EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.5f));
            result.Containers.Add(new Tepsi());
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Suto(38, 40, 4));

            return result;
        }

        public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Fakanal fakanal = eg.Use<Fakanal>();
            IngredientGroup szaraz = fakanal.Osszekeverni(new Liszt(1000f * amount), new So(6.0f * amount), new Oregano(3.0f * amount), new FeketeBorsOrolt(5.0f * amount));
            IngredientGroup nedves = fakanal.Osszekeverni(new Eleszto(14.0f * amount), new Cukor(1.5f * amount), new Viz(0.6f * amount), new OlivaOlaj(0.05f * amount));

            IIngredient pizzateszta = fakanal.Osszekeverni(szaraz, nedves);
            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(pizzateszta);
            edeny.Varni(45);
            pizzateszta = edeny.Kivenni();

            NyujtoDeszka nyd = eg.Use<NyujtoDeszka>();
            pizzateszta = nyd.Nyujtani(pizzateszta, 1);

            result.Add("pizzateszta", pizzateszta);

            eg.WashUp();
            return result;
        }

        public static new CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Tepsi tepsi = eg.Use<Tepsi>();
            tepsi.Berakni(preps["pizzateszta"]);

            Suto suto = eg.Use<Suto>();
            suto.Homerseklet(200);
            suto.Behelyezni(tepsi);
            suto.Varni(30);
            tepsi = suto.Kiemelni<Tepsi>();

            cfp.Add("pizzaalap", tepsi.Contents);

            eg.WashUp();
            return cfp;
        }

        public override void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["pizzateszta"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}