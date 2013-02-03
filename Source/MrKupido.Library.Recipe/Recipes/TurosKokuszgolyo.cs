using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cottage cheese coconut ball")]
    [NameAlias("hun", "túrós kókuszgolyó")]

    public class TurosKokuszgolyo : RecipeBase
    {
        public TurosKokuszgolyo(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }

        public static EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny());
            result.Containers.Add(new LaposTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Reszelo reszelo = eg.Use<Reszelo>();

            IIngredient citromHej = reszelo.Lereszelni(new CitromHej(5.0f * amount));

            result.Add("citromhej", citromHej);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Kez kez = eg.Use<Kez>();

            IIngredient massza = kez.Osszegyurni(new Turo(250.0f * amount), new PorCukor(200.0f * amount), new KokuszReszelek(150.0f * amount), new VaniliasCukor(10.0f * amount), preps["citromhej"]);

            IIngredient golyok = kez.Kiszaggatni(massza, 10.0f);

            // foreach
            kez.GolyovaGyurni(golyok);

            IIngredient megforgatottGolyo = kez.Megforgatni(golyok, new KokuszReszelek(10.0f * amount));
            //

            cfp.Add("golyok", megforgatottGolyo);

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.Talalni(food["golyok"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}
