using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Containers
{
    public class Edeny : RoundContainer
    {
        public Edeny(float scale = 1.0f)
            : base(7.0f * scale, 6.0f * scale)
        {
        }
    }
}
