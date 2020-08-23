using MrKupido.Library.Attributes;
using System;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "knife")]
    [NameAlias("hun", "kés")]
    public class Kes : Tool
    {
        [NameAlias("eng", "cut", Priority = 200)]
        [NameAlias("hun", "feldarabol", Priority = 200)]
        [NameAlias("eng", "cut the {0} into pieces")]
        [NameAlias("hun", "darabold fel a(z) {0T}")]
        [NameAlias("hun", "darabold fel a(z) {0T} kb. {1} dekás darabokra", Priority = 101)]
        public void FeldarabolniI(ISingleIngredient i, float weight)
        {
            weight *= 10; //dkg -> gramm

            if (!i.IsSolid) throw new InvalidActionForIngredientException("Feldarabolni", i);

            if (i.Unit != MeasurementUnit.gramm) i.ChangeUnitTo(MeasurementUnit.gramm);

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.State |= IngredientState.Darabolt;
            i.PieceCount = count;

            this.LastActionDuration = 15 * (uint)count;
        }

        [NameAlias("eng", "cut", Priority = 200)]
        [NameAlias("hun", "feldarabol", Priority = 200)]
        [NameAlias("eng", "cut the {0.Contents.} into {1}-dkg pieces")]
        [NameAlias("hun", "darabold fel a(z) {0.Contents.T} kb. {1} dekás darabokra")]
        public void FeldarabolniC(IIngredientContainer c, float weight)
        {
            weight *= 10; //dkg -> gramm

            IIngredient ci = c.Contents;

            if (!ci.IsSolid) throw new InvalidActionForIngredientException("Feldarabolni", ci);

            if (ci.Unit != MeasurementUnit.gramm) ci.ChangeUnitTo(MeasurementUnit.gramm);

            float totalWeight = ci.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            ci.State |= IngredientState.Darabolt;
            ci.PieceCount = count;

            this.LastActionDuration = 10 * (uint)count;
        }

        [NameAlias("eng", "circle", Priority = 200)]
        [NameAlias("hun", "felkarikáz", Priority = 200)]
        [NameAlias("eng", "circle the {0}")]
        [NameAlias("hun", "karikázd fel a(z) {0T}")]
        public void FelkarikazniI(ISingleIngredient i, float weight)
        {
            weight *= 10; //dkg -> gramm

            if (!i.IsSolid) throw new InvalidActionForIngredientException("Felkarikazni", i);

            i.ChangeUnitTo(MeasurementUnit.gramm);

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.State |= IngredientState.Karikazott;
            i.PieceCount = count;

            this.LastActionDuration = 10 * (uint)count;
        }

        [NameAlias("eng", "peel", Priority = 200)]
        [NameAlias("hun", "meghámoz", Priority = 200)]
        [NameAlias("eng", "peel the {0}")]
        [NameAlias("hun", "hámozd meg a(z) {0T}")]
        public void MeghamozniI(ISingleIngredient i)
        {
            i.State |= IngredientState.Hamozott;

            this.LastActionDuration = 120;
        }

        [NameAlias("eng", "spread", Priority = 200)]
        [NameAlias("hun", "megken", Priority = 200)]
        [NameAlias("eng", "spread the {0.Contents.} with {1}")]
        [NameAlias("hun", "kend meg a(z) {0.Contents.T} a(z) {1.Contents.V}")]
        public void Megkenni(IIngredientContainer c1, IIngredientContainer c2)
        {
            this.LastActionDuration = 20;
        }
    }
}
