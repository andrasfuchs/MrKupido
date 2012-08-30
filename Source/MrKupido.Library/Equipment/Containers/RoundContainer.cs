using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "kör alapú tároló")]

    public class RoundContainer : Container
    {
        public RoundContainer(float radius, float depth) : base(radius*2, radius*2, depth)
        {
        }
    }
}
