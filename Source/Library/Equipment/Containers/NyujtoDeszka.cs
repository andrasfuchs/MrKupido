using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "nyújtódeszka")]

    public class NyujtoDeszka : Container
    {
        public NyujtoDeszka(float scale = 1.0f)
            : base(40.0f * scale, 80.0f * scale, 1.0f)
        {
        }

        public IIngredient Nyujtani(IIngredient i, float thickness)
        {
            return i;
        }
    }
}
