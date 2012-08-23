using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;

namespace MrKupido.Library.Recipe
{
    public class LustaLangos : RecipeBase
    {
        public LustaLangos(float amount) : base(amount)
        {
            float result = 0.0f;

            // preparation
            IngredientGroup tej = Action.Active.Megfuttatni(new IngredientGroup(new IIngredient[] { new Tej(0.5f), new Eleszto(30f) }));
            IngredientGroup teszta = Action.Active.Osszegyurni(new IngredientGroup(new IIngredient[] { tej, new Liszt(1000f), new Tejfol(0.2f), new So(15f), new Viz(0.5f) }));
            teszta = Action.Active.Letakarni(teszta);
            teszta = Action.Active.Homerseklet(24, teszta);
            teszta = Action.Passive.Varni(30, teszta);
            teszta = Action.Active.Nyujtani(5, teszta);
            IngredientGroup tesztadarabok = Action.Active.Kiszaggatni(40, 10, teszta);

            // cooking
            Serpenyo serpenyo = new Serpenyo(26, 4);
            serpenyo = Action.Active.Berakni(serpenyo, new IngredientGroup(new IIngredient[] { new NapraforgoOlaj(0.1f) })) as Serpenyo;
            serpenyo = Action.Active.Homerseklet(350, serpenyo) as Serpenyo;

            IngredientGroup osszeslangos = new IngredientGroup(new IIngredient[] {});

            Action.Passive.Amig(
                () => tesztadarabok.Count > 0,
                delegate()
                {
                    serpenyo = Action.Active.Berakni(serpenyo, tesztadarabok) as Serpenyo;
                    serpenyo = Action.Passive.Varni(5, serpenyo) as Serpenyo;

                    IngredientGroup langosok = Action.Active.Kivenni(serpenyo);
                    osszeslangos = new IngredientGroup(new IIngredient[] { osszeslangos, langosok });
                    result += langosok.Count;
                }
                );

            // serving
            Action.Passive.Talalni(new LaposTanyer(), osszeslangos);

            //return result;
        }
    }
}
