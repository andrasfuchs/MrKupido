using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "tengeri só")]

    public class TengeriSo : So
    {
        public TengeriSo(float amount)
            : base(amount)
        {
        }
    }
}