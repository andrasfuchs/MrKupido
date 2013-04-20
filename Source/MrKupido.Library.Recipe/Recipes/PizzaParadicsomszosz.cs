using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "tomato sauce for pizza")]
    [NameAlias("hun", "paradicsomos pizzaszósz", Priority = 1)]
    [NameAlias("hun", "paradicsomszósz pizzához")]

    [IngredientConsts(IsInline = true)]
    public class PizzaParadicsomszosz : RecipeBase
    {
        public PizzaParadicsomszosz(float amount, MeasurementUnit unit = MeasurementUnit.deciliter)
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

			eg.Use<Serpenyo>(1).BeonteniI(new OlivaOlaj(0.25f));

			ISingleIngredient hagyma = new Hagyma(1.0f * amount);
			eg.Use<Kes>(1).FeldarabolniI(hagyma, 0.1f);
			eg.Use<Serpenyo>(1).BerakniI(hagyma);

			ISingleIngredient fokhagyma = new Fokhagyma(3.0f);
			eg.Use<FokhagymaPres>(1).PreselniI(fokhagyma);
			eg.Use<LaposKisTanyer>(1).RarakniI(fokhagyma);

			result.Add("hagyma", eg.Use<Serpenyo>(1));
			result.Add("fokhagyma", eg.Use<LaposKisTanyer>(1));

			eg.WashUp();
			return result;
		}

		public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
		{
			CookedFoodParts cfp = new CookedFoodParts();

			Serpenyo serpenyo = (Serpenyo)preps["hagyma"];

			eg.Use<Tuzhely>(1).RahelyezniC(serpenyo);
			eg.Use<Tuzhely>(1).Homerseklet(150);
			eg.Use<Tuzhely>(1).Varni(3);

			IIngredientContainer fokhagyma = preps["fokhagyma"];
			serpenyo.Berakni(new So(2.0f * amount), fokhagyma.Contents, new SuritettParadicsom(1.0f * amount), new Viz(0.5f * amount));
			ISingleIngredient baberLevel = new Baberlevel(1.0f * amount);
			serpenyo.Raszorni(new OreganoOrolt(0.5f * amount, MeasurementUnit.teaskanal), new Majoranna(0.5f * amount, MeasurementUnit.teaskanal), new FeketeBorsOrolt(0.5f * amount, MeasurementUnit.teaskanal), new BazsalikomOrolt(0.5f * amount, MeasurementUnit.teaskanal), baberLevel, new Zsalya(0.5f * amount, MeasurementUnit.teaskanal), new Kakukkfu(0.5f * amount, MeasurementUnit.teaskanal), new KristalyCukor(0.5f * amount, MeasurementUnit.teaskanal));

			eg.Use<Tuzhely>(1).Varni(5);

			serpenyo.KivenniI(baberLevel);

			cfp.Add("pizzaszosz", serpenyo);

			return cfp;
		}

		public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
		{
			eg.Use<Kez>(1).TalalniC(food["pizzaszosz"], eg.Use<Bogre>(1));
			eg.WashUp();
		}
    }
}
