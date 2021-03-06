﻿using MrKupido.Library.Attributes;
using MrKupido.Library.Nature;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "mushroom", Priority = 1)]
    [NameAlias("eng", "field mushroom")]
    [NameAlias("eng", "meadow mushroom", Priority = 200)]
    [NameAlias("hun", "gomba", Priority = 1)]
    [NameAlias("hun", "kerti csiperke")]
    [NameAlias("hun", "réti csiperke", Priority = 200)]

    [IngredientConsts(Category = ShoppingListCategory.Mushroom)]

    [PartOf(typeof(AgaricusCampestris))]
    public class KertiCsiperke : SingleIngredient
    {
        public KertiCsiperke(float amount, MeasurementUnit unit = MeasurementUnit.gramm)
            : base(amount, unit)
        {
        }
    }
}
