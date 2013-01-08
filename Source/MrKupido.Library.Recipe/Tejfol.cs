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
    [NameAlias("hun", "tejföl")]

    [NameAlias("eng", "sour cream")]

    [IngredientConsts(GrammsPerLiter = 1750)]
    public class Tejfol : RecipeBase
    {
        public Tejfol(float amount, MeasurementUnit unit = MeasurementUnit.liter)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.0f));
            result.Containers.Add(new Bogre(1.0f));

            result.Devices.Add(new Futotest());
            
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

            Futotest futotest = eg.Use<Futotest>();

            Edeny edeny = eg.Use<Edeny>();
            edeny.Beonteni(new Tej(10.0f * amount));

            futotest.Behelyezni(edeny);
            futotest.Homerseklet(40);
            edeny.Varni((int)(1.5 * 24 * 60));
            edeny = (Edeny)futotest.Kiemelni(typeof(Edeny));

            Spoon kanal = eg.Use<Spoon>();
            IIngredient tejfol = kanal.Lefoloz(edeny, 0.10f);

            cfp.Add("tejfol", tejfol);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["tejfol"], eg.Use<Bogre>());

            eg.WashUp();
        }
    }
}
