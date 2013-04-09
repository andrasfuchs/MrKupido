using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "mustard garlic chicken")]
    [NameAlias("hun", "mustáros fokhagymás csirke")]

    public class MustarosFokhagymasSultCsirkecomb : RecipeBase
    {
        public MustarosFokhagymasSultCsirkecomb(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            ISingleIngredient fokhagyma = new Fokhagyma(3.00f * amount);
            eg.Use<FokhagymaPres>(1).PreselniI(fokhagyma);

            eg.Use<Edeny>(1).Berakni(new Tejfol(4.0f * amount), new Mustar(1.0f * amount), new Etelizesito(1.0f * amount), new FeketeBorsOrolt(5.0f * amount), fokhagyma, new NapraforgoOlaj(1.0f * amount));
            eg.Use<Edeny>(1).Contents.ChangeUnitTo(MeasurementUnit.liter);

            eg.Use<MelyTanyer>(1).BerakniI(new Csirkecomb(8.0f * amount, MeasurementUnit.piece));
            eg.Use<Ecset>(1).BekenniI(new Vaj(5.0f * amount), eg.Use<MelyTanyer>(1));

            eg.Use<Kez>(1).BelemartaniC(eg.Use<MelyTanyer>(1), eg.Use<Edeny>(1));

            result.Add("csirkecomb", eg.Use<MelyTanyer>(1));
            
            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<Tepsi>(1).BerakniC(preps["csirkecomb"]);
            eg.Use<Tepsi>(1).Lefedni(new Alufolia());

            eg.Use<Suto>(1).Homerseklet(200);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(40);
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

            eg.Use<Reszelo>(1).LereszelniI(eg.Use<LaposKisTanyer>(1), new Sajt(20.0f * amount));

            eg.Use<Tepsi>(1).FedotLevenni();
            eg.Use<Kez>(1).RaszorniC(eg.Use<Tepsi>(1), eg.Use<LaposKisTanyer>(1));

            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(10);
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));

            cfp.Add("csirkecomb", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["csirkecomb"], eg.Use<LaposTanyer>(1));
            eg.WashUp();
        }
    }
}
