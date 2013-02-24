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
    [NameAlias("eng", "baked cauliflower")]
    [NameAlias("hun", "tepsis karfiol")]

    public class TepsisKarfiol : RecipeBase
    {
        public TepsisKarfiol(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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
            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            ISingleIngredient karfiol = new Karfiol(6.0f * amount);
            eg.Use<Kez>(1).SzetszedniI(karfiol);

            eg.Use<Edeny>(1).Berakni(new Vaj(50.0f * amount), new Liszt(30.0f * amount));

            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(1));
            eg.Use<Tuzhely>(1).Homerseklet(80);

            eg.Use<NagyEdeny>(1).BeonteniI(new Tej(0.2f * amount));
            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<NagyEdeny>(1));
            eg.Use<Tuzhely>(1).Homerseklet(60);

            eg.Use<NagyEdeny>(1).BerakniC(eg.Use<Edeny>(1));
            eg.Use<Fakanal>(1).KevergetniC(eg.Use<NagyEdeny>(1), 10);

            ISingleIngredient hagyma = new Ujhagyma(1.0f * amount, MeasurementUnit.piece);
            eg.Use<Kes>(1).FelkarikazniI(hagyma, 1.0f);

            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposTanyer>(1), new FustoltSajt(100.0f));

            eg.Use<NagyEdeny>(1).Berakni(new Fuszerpaprika(5.0f * amount), new So(5.0f * amount), new FeherBors(3.0f * amount), new Borokabogyo(2.0f * amount), hagyma);
            eg.Use<NagyEdeny>(1).BerakniC(eg.Use<LaposTanyer>(1));

            eg.Use<Tuzhely>(1).Homerseklet(30);

            eg.Use<NagyEdeny>(1).BerakniI(new Tojas(2.0f * amount));

            eg.Use<NagyEdeny>(1).Varni(2);

            eg.Use<Tepsi>(1).BerakniI(karfiol);
            eg.Use<Tepsi>(1).BeonteniC(eg.Use<NagyEdeny>(1));
            eg.Use<Tepsi>(1).Lefedni(new Alufolia());

            eg.Use<Suto>(1).Homerseklet(200);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));

            eg.Use<Suto>(1).Varni(30);

            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));
            eg.Use<Tepsi>(1).FedotLevenni();
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));

            eg.Use<Suto>(1).Varni(5);

            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

            cfp.Add("karfiol", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>(1);
            kez.TalalniC(food["karfiol"], eg.Use<LaposTanyer>(2));
            eg.WashUp();
        }
    }
}
