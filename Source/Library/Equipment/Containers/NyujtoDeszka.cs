using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Containers
{
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
