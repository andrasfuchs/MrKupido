﻿using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "potato peeler")]
    [NameAlias("hun", "krumplipucoló")]
    public class KrumpliPucolo : Tool
    {
        [NameAlias("eng", "peel", Priority = 200)]
        [NameAlias("hun", "meghámoz", Priority = 200)]
        [NameAlias("eng", "peel the {0}")]
        [NameAlias("hun", "hámozd meg a(z) {0T}")]
        public void MeghamozniI(ISingleIngredient i)
        {
            i.State |= IngredientState.Hamozott;

            this.LastActionDuration = 60 * (uint)i.PieceCount;
        }
    }
}
