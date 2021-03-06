﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "parsley leaf")]
    [NameAlias("hun", "petrezselyem", Priority = 1)]
    [NameAlias("hun", "petrezselyemzöld", Priority = 2)]
    [NameAlias("hun", "petrezselyemlevél")]

    [LeafOf(typeof(Petroselinum))]
    public class Petrezselyem : SingleIngredient
    {
        public Petrezselyem(float amount, MeasurementUnit unit = MeasurementUnit.gramm, IngredientState state = IngredientState.Normal)
            : base(amount, unit, state)
        {
        }
    }
}