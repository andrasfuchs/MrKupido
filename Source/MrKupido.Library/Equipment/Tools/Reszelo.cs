using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "rasper")]
    [NameAlias("hun", "reszelő")]
    public class Reszelo : Tool
    {
        [NameAlias("eng", "rasp", Priority = 200)]
        [NameAlias("hun", "lereszel", Priority = 200)]
        [NameAlias("eng", "rasp the {1} into the {0B}")]
        [NameAlias("hun", "reszeld le a(z) {1T} a(z) {0B}")]
        public void LereszelniI(IIngredientContainer ic, ISingleIngredient i)
        {
            if (!i.IsSolid) throw new InvalidActionForIngredientException("Lereszelni", i);

            i.State |= IngredientState.Reszelt;

            ic.Add(i);

            this.LastActionDuration = 240;
        }
    }
}
