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
    [NameAlias("hun", "tejszín")]
    [NameAlias("hun", "tejzsír", Priority=200)]

    [NameAlias("eng", "cream")]

    [IngredientConsts(GrammsPerLiter = 1900, IsIngrec = true)]
    public class Tejszin : RecipeBase
    {
        public Tejszin(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new Bogre(1.0f));

            result.Devices.Add(new Tuzhely());
            result.Devices.Add(new Hutogep());

            result.Tools.Add(new Kez());
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

            Edeny edeny1 = eg.Use<Edeny>();
            edeny1.Beonteni(new Tej(12.0f * amount, MeasurementUnit.liter));
            edeny1.Varni(180);

            Spoon kanal = eg.Use<Spoon>();
            IIngredient nyersTejszin = kanal.Lefoloz(edeny1, 0.15f);

            Edeny edeny2 = eg.Use<Edeny>();
            edeny2.Beonteni(nyersTejszin);

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Behelyezni(edeny2);
            tuzhely.Homerseklet(110);
            edeny2.Varni(10);

            edeny2 = (Edeny)tuzhely.Kiemelni(typeof(Edeny));
            edeny2.Varni(30);

            Hutogep huto = eg.Use<Hutogep>();
            huto.Behelyezni(edeny2);
            edeny2.Varni(60);

            cfp.Add("tejszin", edeny2);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["tejszin"], eg.Use<Bogre>());

            eg.WashUp();
        }
    }
}
