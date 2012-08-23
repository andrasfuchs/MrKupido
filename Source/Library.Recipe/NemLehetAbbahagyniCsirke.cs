using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Recipe
{
    class NemLehetAbbahagyniCsirke : RecipeBase
    {
        public NemLehetAbbahagyniCsirke(float amount) : base(amount)
        {
            float result = 0.0f;

            // preparation
            IngredientGroup csirkemell = Action.Active.Feldarabolni(50.0f, new Csirkemell(500.0f));
            csirkemell = Action.Active.Raszorni(new So(5.0f), csirkemell);
            IngredientGroup fuszeresliszt = Action.Active.Osszekeverni(new IngredientGroup(new IIngredient[] { new Liszt(70.0f), new So(5.0f), new Fuszerpaprika(5.0f), new FeketeBors(3.0f), new Majoranna(3.0f) }));
            csirkemell = Action.Active.Megforgatni(fuszeresliszt, csirkemell);

            IngredientGroup hagyma = Action.Active.Felkarikazni(5.0f, new Hagyma(35.0f));
            IngredientGroup tejfol = Action.Active.Osszekeverni(new IngredientGroup(new IIngredient[] { new NapraforgoOlaj(0.1f), new Tejfol(0.2f) }));
            IngredientGroup sajt = Action.Active.Lereszelni(new Sajt(100.0f));
            
            // cooking
            Tepsi tepsi = new Tepsi(30, 34, 2);
            tepsi = Action.Active.Berakni(tepsi, csirkemell) as Tepsi;
            tepsi.Contents = Action.Active.Raszorni(new Liszt(10.0f), tepsi.Contents);
            tepsi.Contents = Action.Active.Rarakni(hagyma, tepsi.Contents);
            tepsi.Contents = Action.Active.Lelocsolni(tejfol, tepsi.Contents);
            tepsi.Contents = Action.Active.Raszorni(sajt, tepsi.Contents);
            Alufolia alufolia = new Alufolia(29.0f, 1000.0f);
            tepsi = Action.Active.Letakarni(alufolia, tepsi) as Tepsi;

            Suto suto = new Suto(38, 40, 4);
            suto = Action.Active.Homerseklet(200, suto) as Suto;
            suto = Action.Active.BerakniTarolot(suto, tepsi) as Suto;
            suto = Action.Passive.Varni(30, suto) as Suto;

            tepsi = Action.Active.KivenniTarolot(suto) as Tepsi;
            tepsi = Action.Active.Levenni(alufolia, tepsi) as Tepsi;

            suto = Action.Active.BerakniTarolot(suto, tepsi) as Suto;
            suto = Action.Passive.Varni(10, suto) as Suto;
            tepsi = Action.Active.KivenniTarolot(suto) as Tepsi;

            // serving
            Action.Passive.Talalni(new LaposTanyer(), tepsi.Contents);

            //return result;
        }
    }
}
