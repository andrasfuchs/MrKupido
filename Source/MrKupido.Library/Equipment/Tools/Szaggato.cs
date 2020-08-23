using MrKupido.Library.Attributes;
using System;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "chopper")]
    [NameAlias("hun", "szaggató")]
    public class Szaggato : Tool
    {
        [NameAlias("eng", "chop", Priority = 200)]
        [NameAlias("hun", "kiszaggat", Priority = 200)]
        [NameAlias("eng", "chop approx. {1}-dkg pieces with {2}-cm diameter from the {0}")]
        [NameAlias("hun", "szaggass ki kb. {1} dekás, {2} cm átmérőjű alakzatokat a(z) {0H}")]
        public void KiszaggatniC(IIngredientContainer ic, float weight, float diameter)
        {
            weight *= 10; // dkg -> g
            diameter *= 10; // cm -> mm

            float totalWeight = ic.Contents.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            ic.Contents.PieceCount = count;

            LastActionDuration = 30 * (uint)count;
        }
    }
}
