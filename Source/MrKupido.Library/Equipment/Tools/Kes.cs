using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "knife")]
    [NameAlias("hun", "kés")]
    public class Kes : Tool
    {
        [NameAlias("eng", "dismember", Priority = 200)]
        [NameAlias("hun", "feldarabol", Priority = 200)]
        [NameAlias("hun", "darabold fel a(z) {0T} {1} grammos darabokra")]
        public void Feldarabolni(ISingleIngredient i, float weight)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Feldarabolni", i.Name, i.Unit);

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.State = IngredientState.Darabolt;
            i.PieceCount = count;

            this.LastActionDuration = 10 * (uint)count;
        }

        [NameAlias("eng", "circle", Priority = 200)]
        [NameAlias("hun", "felkarikáz", Priority = 200)]
        [NameAlias("hun", "karikázd fel a(z) {0T} {1} grammos darabokra")]
        public void Felkarikazni(ISingleIngredient i, float weight)
        {
            if ((i.Unit != MeasurementUnit.piece) && (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Felkarikazni", i.Name, i.Unit);

            i.ChangeUnitTo(MeasurementUnit.gramm);

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.State = IngredientState.Karikazott;
            i.PieceCount = count;

            this.LastActionDuration = 10 * (uint)count;
        }
    }
}
