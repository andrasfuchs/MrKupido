using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "pan")]
    [NameAlias("hun", "serpenyő")]
    public class Serpenyo : RoundContainer
    {
        public Serpenyo(float scale = 1.0f)
            : base(26.0f * scale, 4.0f)
        {
        }

        [NameAlias("eng", "bake", Priority = 200)]
        [NameAlias("hun", "kisüt", Priority = 200)]
        [NameAlias("hun", "süsd ki a(z) {N} az összes {0T} egyenként {1} percig")]
        public IIngredient KisutniOsszeset(IIngredientContainer i, float timeForOne)
        {
            IIngredient c = i.Contents;

            if (c is SingleIngredient)
            {
                LastActionDuration = (uint)Math.Ceiling(((SingleIngredient)c).PieceCount * timeForOne * 60);
            }
            else if (c is IngredientGroup)
            {
                LastActionDuration = (uint)Math.Ceiling(((IngredientGroup)c).Count * timeForOne * 60);
            }

            i.Empty();

            return c;
        }
    }
}
