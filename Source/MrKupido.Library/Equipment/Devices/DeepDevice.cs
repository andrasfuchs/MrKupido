using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "deep device")]
    [NameAlias("hun", "mély berendezés")]
    public class DeepDevice : Device
    {
        public DeepDevice(float width, float height, float depth)
            : base(width, height, depth)
        {
        }

        [NameAlias("eng", "put in", Priority = 200)]
        [NameAlias("hun", "behelyez", Priority = 200)]
		[NameAlias("eng", "put the {0} into the {}")]
        [NameAlias("hun", "helyezd be a(z) {B} a(z) {0T}")]
        public void BehelyezniC(IIngredientContainer c)
        {
            this.contents.Add(c);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "pull out", Priority = 200)]
        [NameAlias("hun", "kiemel", Priority = 200)]
		[NameAlias("eng", "pull the {0} out of the {}")]
        [NameAlias("hun", "emeld ki a(z) {K} a(z) {0T}")]
        public void KiemelniC(IIngredientContainer c)
        {
            if (this.contents.Count == 0) throw new MrKupidoException("The device '{0}' is empty at the moment.", this.Name);
            if (!this.contents.Contains(c)) throw new MrKupidoException("The device '{0}' doesn't contain the container you requested.", this.Name);

            this.contents.Remove(c);

            this.LastActionDuration = 60;
        }
    }
}
