﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "potato squeeze")]
    [NameAlias("hun", "burgonyaprés")]
    public class BurgonyaPres : Tool
    {
        [NameAlias("eng", "squash", Priority = 200)]
        [NameAlias("hun", "összeprésel", Priority = 200)]
        [NameAlias("eng", "squash the {0}")]
        [NameAlias("hun", "préseld össze a(z) {0T}")]
        public void PreselniI(ISingleIngredient i)
        {
            if (!i.IsSolid) throw new InvalidActionForIngredientException("Preselni", i);

            i.State |= IngredientState.Preselt;

            this.LastActionDuration = 120;
        }
    }
}
