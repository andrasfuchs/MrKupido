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
            result.Containers.Add(new LaposKisTanyer());
            result.Containers.Add(new LaposTanyer());
            result.Containers.Add(new MelyTanyer());

            result.Tools.Add(new Kez());
            result.Tools.Add(new Reszelo());

            return result;
        }

        public static PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            Reszelo reszelo = eg.Use<Reszelo>();
            LaposKisTanyer tanyer = eg.Use<LaposKisTanyer>();

            //IIngredient citromHej = reszelo.Lereszelni(new CitromHej(5.0f * amount));
            reszelo.LereszelniI(tanyer, new CitromHej(5.0f * amount));

            result.Add("citromhej", tanyer);

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            IIngredientContainer kisTanyer = preps["citromhej"];

            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(new Turo(250.0f * amount), new PorCukor(20.0f * amount), new KokuszReszelek(150.0f * amount), new VaniliasCukor(10.0f * amount), kisTanyer.Contents);

            Kez kez = eg.Use<Kez>();

            kez.OsszegyurniC(edeny);

            // foreach
			kez.GolyovaGyurniC(edeny, 50.0f, eg.Use<LaposTanyer>(1));

			eg.Use<MelyTanyer>(1).BerakniI(new KokuszReszelek(100.0f * amount));
			kez.MegforgatniC(eg.Use<MelyTanyer>(1), eg.Use<LaposTanyer>(1), eg.Use<MelyTanyer>(2));
            //

			cfp.Add("golyok", eg.Use<MelyTanyer>(2));
             
            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Kez kez = eg.Use<Kez>();
            kez.TalalniC(food["golyok"], eg.Use<LaposTanyer>());
            eg.WashUp();
        }
    }
}
