using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment.Containers
{
    public class RoundContainer : Container
    {
        public RoundContainer(float radius, float depth) : base(radius*2, radius*2, depth)
        {
        }
    }
}
