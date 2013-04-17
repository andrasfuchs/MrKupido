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
    [NameAlias("eng", "muffin with cinemon")]
    [NameAlias("hun", "fahéjas muffin")]

	[IngredientConsts(ManTags = "Cake")]
    public class FahejasMuffin : RecipeBase
    {
        public FahejasMuffin(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();
            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            eg.Use<Edeny>(1).BerakniI(new Vaj(10.0f * amount));
            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(1));
            eg.Use<Tuzhely>(1).Homerseklet(50);
            eg.Use<Tuzhely>(1).LeemelniC(eg.Use<Edeny>(1));


            eg.Use<Ecset>(1).KikenniC(eg.Use<Edeny>(1), eg.Use<MuffinForma>(1));

            ISingleIngredient alma = new Alma(3.0f * amount);
            eg.Use<KrumpliPucolo>(1).MeghamozniI(alma);
            eg.Use<Kes>(1).FeldarabolniI(alma, 2.0f);

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(25.0f * amount), new Cukor(8.0f * amount), new Sutopor(5.0f * amount), new Fahej(8.0f * amount), new So(5.0f * amount), alma);
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<NagyEdeny>(1));

            IIngredient tojasfeherje = eg.Use<Kez>(1).SzetvalasztaniI(new Tojas(2.0f * amount))[1];
            eg.Use<Habvero>(1).FelverniI(tojasfeherje);
            eg.Use<Edeny>(2).Berakni(tojasfeherje, new Vaj(2.5f * amount), new Tej(0.5f * amount));

            // Kanalanként a lisztes-almás keverékbe dolgozzuk úgy hogy ne túl folyékony masszát kapjunk
            eg.Use<NagyEdeny>(2).BeonteniC(eg.Use<Edeny>(2));

            result.Add("muffinmassza", eg.Use<Edeny>(2));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<Suto>(1).Homerseklet(200);

            eg.Use<MuffinForma>(1).BerakniC(preps["muffinmassza"]);

            eg.Use<Suto>(1).BehelyezniC(eg.Use<MuffinForma>(1));
            eg.Use<Suto>(1).Varni(30);
            eg.Use<Suto>(1).KiemelniC(eg.Use<MuffinForma>(1));

            eg.Use<Kez>(1).RaszorniI(eg.Use<MuffinForma>(1), new Fahej(2.0f * amount));

            cfp.Add("muffin", eg.Use<MuffinForma>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>(1);
            kez.TalalniC(food["muffin"], eg.Use<LaposKisTanyer>(1));
            eg.WashUp();
        }
    }
}
