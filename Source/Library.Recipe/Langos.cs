using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Equipment;
using MrKupido.Library.Equipment.Containers;
using MrKupido.Library.Attributes;
using MrKupido.Library.Equipment.Tools;
using MrKupido.Library.Equipment.Devices;

namespace MrKupido.Library.Recipe
{
    [NameAlias("hun", "lángos")]

    public class Langos : RecipeBase
    {
        public Langos(float amount) : base(amount)
        {
        }

        public override EquipmentGroup SelectEquipment(float amount)
        {
            EquipmentGroup result = new EquipmentGroup();

            result.Containers.Add(new Edeny(1.5f));
            result.Containers.Add(new Serpenyo());
            result.Containers.Add(new LaposTanyer());

            result.Devices.Add(new Suto(38, 40, 4));

            return result;
        }

        public override PreparedIngredients Prepare(float amount, EquipmentGroup eg)
        {
            PreparedIngredients result = new PreparedIngredients();

            IIngredient tej = Action.Active.Megfuttatni(new IngredientGroup(new IIngredient[] { new Tej(0.5f * amount), new Eleszto(30f * amount) }));
            IIngredient teszta = Action.Active.Osszegyurni(new IngredientGroup(new IIngredient[] { tej, new Liszt(1000f * amount), new Tejfol(0.2f * amount), new So(15f * amount), new Viz(0.5f * amount) }));
            
            Edeny edeny = eg.Use<Edeny>();
            edeny.Berakni(teszta);
            edeny.Lefedni();
            edeny.Varni(30);

            NyujtoDeszka nyd = eg.Use<NyujtoDeszka>();
            teszta = nyd.Nyujtani(teszta, 5);

            Szaggato szaggato = eg.Use<Szaggato>();
            IIngredient tesztadarabok = szaggato.Kiszaggatni(teszta, 40, 10);

            result.Add("tesztadarabok", tesztadarabok);

            return result;
        }

        public override CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            Serpenyo serpenyo = eg.Use<Serpenyo>();
            serpenyo.Berakni(new IngredientGroup(new IIngredient[] { new NapraforgoOlaj(0.1f) }));

            Tuzhely tuzhely = eg.Use<Tuzhely>();
            tuzhely.Behelyezni(serpenyo);
            tuzhely.Homerseklet(350);

            IngredientGroup osszeslangos = new IngredientGroup(new IIngredient[] { });

            Action.Passive.Amig(
                () => ((IngredientGroup)preps["tesztadarabok"]).Count > 0,
                delegate()
                {
                    serpenyo.Berakni(preps["tesztadarabok"]);
                    serpenyo.Varni(5);

                    IIngredient langosok = serpenyo.Kivenni();
                    osszeslangos = new IngredientGroup(new IIngredient[] { osszeslangos, langosok });
                }
                );

            cfp.Add("osszeslangos", osszeslangos);

            return cfp;
        }

        public override void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            Action.Passive.Talalni(eg.Use<LaposTanyer>(), food["osszeslangos"]);
        }
    }
}
