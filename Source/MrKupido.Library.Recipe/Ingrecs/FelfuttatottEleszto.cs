using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "baker's yeast")]
    [NameAlias("hun", "felfuttatott élesztő")]

    [IngredientConsts(IsIngrec = true, IsInline=true)]
    public class FelfuttatottEleszto : RecipeBase
    {
        public FelfuttatottEleszto(float amount, MeasurementUnit unit = MeasurementUnit.dekagramm)
            : base(amount, unit)
        {
        }

		public static new EquipmentGroup SelectEquipment(float amount)
		{
			EquipmentGroup result = new EquipmentGroup();
			return result;
		}

		public static new PreparedIngredients Prepare(float amount, EquipmentGroup eg)
		{
			PreparedIngredients result = new PreparedIngredients();

			eg.Use<Edeny>(1).BeonteniI(new Tej(0.5f * amount));
			eg.Use<Kez>(1).Morzsol(new Eleszto(1.0f * amount), eg.Use<Edeny>(1));

			eg.Use<Futotest>(1).RahelyezniC(eg.Use<Edeny>(1));
			eg.Use<Edeny>(1).Varni(20);

			result.Add("eleszto", eg.Use<Edeny>(1));

			eg.WashUp();
			return result;
		}

		public static new CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
		{
			CookedFoodParts cfp = new CookedFoodParts();
			cfp.Add("eleszto", preps["eleszto"]);
			return cfp;
		}

		public static new void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
		{
			eg.Use<Kez>(1).TalalniC(food["eleszto"], eg.Use<Bogre>(1));
			eg.WashUp();
		}
    }
}
