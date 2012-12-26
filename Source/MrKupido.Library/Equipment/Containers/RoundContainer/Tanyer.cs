using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "plate")]
    [NameAlias("hun", "tányér")]
    public class Tanyer : RoundContainer
    {
        public Tanyer(float radius, float depth)
            : base(radius, depth)
        {
        }
    }
}
