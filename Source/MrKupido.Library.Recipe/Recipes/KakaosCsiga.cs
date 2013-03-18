﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library;
using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Recipe
{
    [NameAlias("eng", "cocoa spiral")]
    [NameAlias("hun", "kakaós csiga")]

    public class KakaosCsiga : RecipeBase
    {
        public KakaosCsiga(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
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

            eg.Use<NagyEdeny>(1).Berakni(new Liszt(400.0f * amount), new TojasSargaja(2.0f * amount), new FelfuttatottEleszto(5.0f * amount), new Vaj(40.00f * amount), new Cukor(40.0f * amount), new So(2.0f * amount));
            eg.Use<Kez>(1).OsszegyurniC(eg.Use<NagyEdeny>(1));

            eg.Use<Futotest>(1).RahelyezniC(eg.Use<NagyEdeny>(1));
            eg.Use<Futotest>(1).Varni(45);
            eg.Use<Futotest>(1).LeemelniC(eg.Use<NagyEdeny>(1));

            result.Add("csigateszta", eg.Use<NagyEdeny>(1));

            eg.WashUp();
            return result;
        }

        public static CookedFoodParts Cook(float amount, PreparedIngredients preps, EquipmentGroup eg)
        {
            CookedFoodParts cfp = new CookedFoodParts();

            eg.Use<NyujtoDeszka>(1).NyujtaniC(preps["csigateszta"], 1.50f);

            eg.Use<Edeny>(1).Berakni(new PorCukor(140.0f * amount), new VaniliasCukor(20.0f * amount), new Vaj(100.0f * amount), new Kakaopor(20.0f * amount));
            eg.Use<Fakanal>(1).ElkeverniC(eg.Use<Edeny>(1));

            eg.Use<Ecset>(1).MegkenniC(eg.Use<Edeny>(1), eg.Use<NyujtoDeszka>(1));
            eg.Use<Kez>(1).FelgongyolniC(eg.Use<NyujtoDeszka>(1));

            eg.Use<Kes>(1).FeldarabolniC(eg.Use<NyujtoDeszka>(1), 10.0f);


            eg.Use<Tepsi>(1).Kibelelni(new Sutopapir());
            eg.Use<Tepsi>(1).BerakniC(eg.Use<NyujtoDeszka>(1));
            eg.Use<Ecset>(1).MegkenniI(new TojasSargaja(2.0f * amount), eg.Use<Tepsi>(1));
            eg.Use<Tepsi>(1).Lefedni(new Konyharuha());
            eg.Use<Tepsi>(1).Varni(30);

            eg.Use<Suto>(1).Homerseklet(200);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(40);
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));


            eg.Use<Edeny>(2).BeonteniI(new Tejszin(0.2f * amount));
            ISingleIngredient cukor = new Cukor(50.0f * amount);
            cukor.ChangeUnitTo(MeasurementUnit.liter);
            eg.Use<Edeny>(2).BerakniI(cukor);

            eg.Use<Tuzhely>(1).RahelyezniC(eg.Use<Edeny>(2));
            eg.Use<Tuzhely>(1).Homerseklet(50);
            eg.Use<Tuzhely>(1).Varni(3);
            eg.Use<Tuzhely>(1).LeemelniC(eg.Use<Edeny>(2));

            eg.Use<Kez>(1).RaonteniC(eg.Use<Tepsi>(1), eg.Use<Edeny>(2));
            eg.Use<Suto>(1).Homerseklet(100);
            eg.Use<Suto>(1).BehelyezniC(eg.Use<Tepsi>(1));
            eg.Use<Suto>(1).Varni(10);
            eg.Use<Suto>(1).KiemelniC(eg.Use<Tepsi>(1));


            cfp.Add("csiga", eg.Use<Tepsi>(1));

            eg.WashUp();
            return cfp;
        }

        public static void Serve(float amount, CookedFoodParts food, EquipmentGroup eg)
        {
            eg.Use<Kez>(1).TalalniC(food["csiga"], eg.Use<LaposTanyer>(1));
            eg.WashUp();
        }
    }
}