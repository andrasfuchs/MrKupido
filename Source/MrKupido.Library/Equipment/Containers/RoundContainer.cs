using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "round shaped container")]
    [NameAlias("hun", "kör alapú tároló")]
    public class RoundContainer : Container
    {
        public RoundContainer(float diameter, float depth)
            : base(diameter, diameter, depth)
        {
        }
    }
}
