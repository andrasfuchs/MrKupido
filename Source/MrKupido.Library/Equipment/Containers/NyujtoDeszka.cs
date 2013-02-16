using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "providing board")]
    [NameAlias("hun", "nyújtódeszka")]
    public class NyujtoDeszka : Container
    {
        [NameAlias("hun", "{L} a tészta")]
        public IIngredient Contents
        {
            get
            {
                return getContents();
            }
        }

        public NyujtoDeszka()
            : this(1.0f)
        { }

        public NyujtoDeszka(float scale)
            : base(40.0f * scale, 80.0f * scale, 1.0f)
        {
        }

        [NameAlias("eng", "roll out", Priority = 200)]
        [NameAlias("hun", "kinyújt", Priority = 200)]
        [NameAlias("hun", "nyújtsd ki nyújtódeszkán a(z) {0.Contents.T} {1} mm-esre")]
        public void Nyujtani(IIngredientContainer c, float thickness)
        {
            this.Add(c.Contents);
            c.Empty();

            this.LastActionDuration = 180;
        }
    }
}
