using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cheese ball")]
    [NameAlias("hun", "sajtgolyó")]

    public class Sajtgolyo : RecipeBase
    {
        public Sajtgolyo(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        { }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();
            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            eg.Use<Labas>(1).Berakni(new Burgonya(2.0f * amount), new Viz(5.0f * amount));

            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Labas>(1));
            eg.Use<Tuzhely>(1).Homerseklet(350);
			eg.Use<Labas>(1).Varni(new Quantity(20, MeasurementUnit.minute));
            eg.Use<Tuzhely>(1).LeemelniC(eg.Use<Labas>(1));
            eg.Use<Labas>(1).FolyadekotLeonteni();

            ISingleIngredient burgonya = (ISingleIngredient)eg.Use<Labas>(1).Kivenni();
            eg.Use<KrumpliPucolo>(1).MeghamozniI(burgonya);
            burgonya.ChangeUnitTo(MeasurementUnit.gramm);

			eg.Use<BurgonyaPres>(1).PreselniI(burgonya);

            ISingleIngredient fokhagyma = new Fokhagyma(1.0f * amount);
			eg.Use<FokhagymaPres>(1).PreselniI(fokhagyma);

            ISingleIngredient sajt = new Sajt(14.0f * amount);
            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposKisTanyer>(1), sajt);

            IIngredient tojas = new Tojas(1.0f * amount);
            //IIngredient tojasfeherje = eg.Use<Kez>(1).SzetvalasztaniI(tojas)[1];
			tojas = eg.Use<Habvero>(1).FelverniI(tojas);
		
			ISingleIngredient vaj = new Vaj(4.0f * amount);
			eg.Use<MikrohullamuSuto>(1).Megolvasztani(vaj);

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(8.0f * amount), new FeketeBorsOrolt(1.0f * amount, MeasurementUnit.teaskanal), new So(1.0f * amount, MeasurementUnit.teaskanal), vaj, burgonya, tojas, fokhagyma, sajt);
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<NagyEdeny>(1));

            //IngredientGroup golyok = kez.Kiszaggatni(eg.Use<NagyEdeny>(1).Contents, 30.0f);
			eg.Use<Kez>(1).GolyovaGyurniC(eg.Use<NagyEdeny>(1), 3.0f, eg.Use<LaposTanyer>(1));

			result.Add("golyok", eg.Use<LaposTanyer>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<Serpenyo>(1).BerakniI(new NapraforgoOlaj(3.0f));

            eg.Use<Tuzhely>(1).Homerseklet(350);
            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Serpenyo>(1));

            eg.Use<MelyTanyer>(1).BerakniI(new Zsemlemorzsa(100.0f));

            //IIngredient golyok = preps["golyok"].Contents;
			eg.Use<Kez>(1).MegforgatniC(eg.Use<MelyTanyer>(1), preps["golyok"], eg.Use<MelyTanyer>(3));

			eg.Use<Serpenyo>(1).KisutniOsszesetC(eg.Use<MelyTanyer>(3), 3.0f, eg.Use<MelyTanyer>(2));

			eg.Use<Kez>(1).LecsepegtetniC(eg.Use<MelyTanyer>(2));

			cfp.Add("osszesgolyo", eg.Use<MelyTanyer>(2));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
			eg.Use<Kez>(1).TalalniC(food["osszesgolyo"], eg.Use<LaposTanyer>(1));

            eg.WashUp();
        }
    }
}
