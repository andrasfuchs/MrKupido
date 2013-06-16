using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "sour cream")]
    [NameAlias("hun", "tejföl")]

	[IngredientConsts(
		IsIngrec = true,
		Category = ShoppingListCategory.Other,
		GrammsPerLiter = 946,
		CaloriesPer100Gramms = 193.0f,
		CarbohydratesPer100Gramms = 10.8f,
		FatPer100Gramms = 173.0f,
		ProteinPer100Gramms = 8.8f,
		GlichemicalIndex = 2,
		InflammationFactor = -111
	)]

    public class Tejfol : RecipeBase
    {
        public Tejfol(float amount, MeasurementUnit unit = MeasurementUnit.deciliter)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new Bogre());

            result.Devices.Add(new Futotest());
            
            result.Tools.Add(new Kez());
            result.Tools.Add(new Kanal());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Futotest futotest = eg.Use<Futotest>();

            Edeny edeny = eg.Use<Edeny>();
            edeny.Beonteni(new Tej(10.0f * amount));

            futotest.RahelyezniC(edeny);
            //futotest.Homerseklet(40);
            edeny.Varni(new Quantity(1.5f, MeasurementUnit.day));
            futotest.LeemelniC(edeny);

            Kanal kanal = eg.Use<Kanal>();
            IIngredient tejfol = kanal.LefolozniC(edeny, 0.10f);

            eg.Use<Bogre>(1).Berakni(tejfol);

            cfp.Add("tejfol", eg.Use<Bogre>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["tejfol"], eg.Use<Bogre>());

            eg.WashUp();
        }
    }
}
