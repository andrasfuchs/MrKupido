﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "egg-beater")]
    [NameAlias("hun", "habverő")]
    public class Habvero : Tool
    {
        [NameAlias("eng", "hunt out", Priority = 200)]
        [NameAlias("hun", "felver", Priority = 200)]
        [NameAlias("eng", "hunt the {0} out")]
        [NameAlias("hun", "verd fel a(z) {0T}")]
        public IIngredient FelverniI(IIngredient i)
        {
            if (!(i is Tojas)) throw new InvalidActionForIngredientException("Felverni", i);

            i.ChangeUnitTo(MeasurementUnit.gramm);

            i.State |= IngredientState.Felvert;

            this.LastActionDuration = 120;

            return i;
        }

        [NameAlias("eng", "mix together", Priority = 200)]
        [NameAlias("hun", "összekever", Priority = 200)]
        [NameAlias("eng", "mix the {0} together")]
        [NameAlias("hun", "alaposan keverd össze a(z) {0T}")]
        public IIngredientGroup ElkeverniIG(IIngredientGroup ingredients)
        {
            this.LastActionDuration = 60;

            return ingredients;
        }

        [NameAlias("eng", "mix up", Priority = 200)]
        [NameAlias("hun", "kikever", Priority = 200)]
        [NameAlias("eng", "mix the {0.Contents.} up")]
        [NameAlias("hun", "keverd ki a(z) {0.Contents.T}")]
        public void KikeverniC(IIngredientContainer c)
        {
            this.LastActionDuration = 60;
        }

    }
}
