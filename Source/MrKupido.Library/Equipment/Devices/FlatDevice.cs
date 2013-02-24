using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "flat device")]
    [NameAlias("hun", "lapos berendezés")]
    public class FlatDevice : Device
    {
        public FlatDevice(float width, float height, float depth)
            : base(width, height, depth)
        {
        }

        [NameAlias("eng", "put on", Priority = 200)]
        [NameAlias("hun", "ráhelyez", Priority = 200)]
        [NameAlias("hun", "helyezd rá a(z) {R} a(z) {0T}")]
        public void RahelyezniC(IIngredientContainer c)
        {
            this.contents.Add(c);

            this.LastActionDuration = 10;
        }

        [NameAlias("eng", "get off", Priority = 200)]
        [NameAlias("hun", "leemel", Priority = 200)]
        [NameAlias("hun", "emeld le a(z) {L} a(z) {0T}")]
        public void LeemelniC(IIngredientContainer c)
        {
            if (this.contents.Count == 0) throw new MrKupidoException("The device '{0}' is empty at the moment.", this.Name);
            if (!this.contents.Contains(c)) throw new MrKupidoException("The device '{0}' doesn't contain the container you requested.", this.Name);

            this.contents.Remove(c);

            this.LastActionDuration = 10;
        }
    }
}
