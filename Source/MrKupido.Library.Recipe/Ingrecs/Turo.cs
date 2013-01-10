using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cottage cheese")]
    [NameAlias("hun", "túró")]

    [IngredientConsts(IsIngrec = true)]
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
            result.Tools.Add(new Spoon());

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

            futotest.Behelyezni(edeny);
            futotest.Homerseklet(40);
            edeny.Varni((int)(1.5 * 24 * 60));
            edeny = (Edeny)futotest.Kiemelni(typeof(Edeny));

            Spoon kanal = eg.Use<Spoon>();
            IIngredient tejfol = kanal.Lefoloz(edeny, 0.10f);


            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Homerseklet(90);
            tuzhely.Behelyezni(edeny);
            Fakanal fakanal = eg.Use<Fakanal>();
            fakanal.Kevergetni(edeny.Contents);
            edeny.Varni(30); // itt keletkezik a turo a tejből

            edeny = (Edeny)tuzhely.Kiemelni(typeof(Edeny));
            edeny.Varni(10);
            edeny.FolyadekotLeonteni();

            cfp.Add("turo", edeny.Contents);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["turo"], eg.Use<LaposTanyer>());

            eg.WashUp();
        }
    }
}