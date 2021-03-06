﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "baker's yeast")]
    [NameAlias("hun", "felfuttatott élesztő")]

    [IngredientConsts(IsIngrec = true, IsInline = true)]
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

            eg.Use<Pohar>(1).BeonteniI(new Tej(0.5f * amount, MeasurementUnit.deciliter, IngredientState.Langyos));
            eg.Use<Pohar>(1).BerakniI(new Cukor(0.8f * amount, MeasurementUnit.teaskanal));
            eg.Use<Kanal>(1).ElkeverniC(eg.Use<Pohar>(1));
            eg.Use<Kez>(1).Morzsol(new Eleszto(1.0f * amount), eg.Use<Pohar>(1));

            eg.Use<Futotest>(1).RahelyezniC(eg.Use<Pohar>(1));
            eg.Use<Pohar>(1).Varni(new Quantity(20, MeasurementUnit.minute));

            result.Add("eleszto", eg.Use<Pohar>(1));

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
