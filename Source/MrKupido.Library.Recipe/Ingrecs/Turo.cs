﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cottage cheese")]
    [NameAlias("hun", "túró")]

    [IngredientConsts(
        IsIngrec = true,
        Category = ShoppingListCategory.Other,
        CaloriesPer100Gramms = 98.0f,
        CarbohydratesPer100Gramms = 12.7f,
        FatPer100Gramms = 37.8f,
        ProteinPer100Gramms = 47.5f,
        GlichemicalIndex = 3,
        InflammationFactor = -22
    )]
    public class Turo : RecipeBase
    {
        public Turo(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Futotest());
            result.Devices.Add(new Tuzhely());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Fakanal());
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
            edeny.Varni(new Quantity(1.5f, MeasurementUnit.minute));
            futotest.LeemelniC(edeny);

            Kanal kanal = eg.Use<Kanal>();
            IIngredient tejfol = kanal.LefolozniC(edeny, 0.10f);


            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Homerseklet(90);
            tuzhely.RahelyezniC(edeny);
            Fakanal fakanal = eg.Use<Fakanal>();
            fakanal.KevergetniC(edeny, 1);
            edeny.Varni(new Quantity(30, MeasurementUnit.minute)); // itt keletkezik a turo a tejből

            tuzhely.LeemelniC(edeny);
            edeny.Varni(new Quantity(10, MeasurementUnit.minute));
            edeny.FolyadekotLeonteni();

            cfp.Add("turo", edeny);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["turo"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}