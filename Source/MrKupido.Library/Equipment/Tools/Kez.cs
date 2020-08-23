using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "hand")]
    [NameAlias("hun", "kéz")]
    public class Kez : Tool
    {
        [NameAlias("eng", "disassemble", Priority = 200)]
        [NameAlias("hun", "szétszed", Priority = 200)]
        [NameAlias("eng", "disassemble the {0}")]
        [NameAlias("hun", "szedd szét a {0T}")]
        public void SzetszedniI(ISingleIngredient i)
        {
            i.PieceCount = 10;
            i.State |= IngredientState.Szetszedett;

            this.LastActionDuration = 180;
        }

        [NameAlias("eng", "crumple", Priority = 200)]
        [NameAlias("hun", "összegyúr", Priority = 200)]
        [NameAlias("eng", "crumple the {0.Contents.}")]
        [NameAlias("hun", "gyúrd össze a(z) {0.Contents.T}")]
        public void OsszegyurniC(IIngredientContainer container)
        {
            this.LastActionDuration = 300;
        }

        [NameAlias("eng", "flip over", Priority = 200)]
        [NameAlias("hun", "megfordít", Priority = 200)]
        [NameAlias("eng", "flip the {0.Contents.} over")]
        [NameAlias("hun", "fordítsd meg a(z) {0.Contents.T}")]
        public void MegforditaniC(IIngredientContainer container)
        {
            this.LastActionDuration = 120;
        }

        [NameAlias("eng", "knead to balls", Priority = 200)]
        [NameAlias("hun", "golyóvá gyúr", Priority = 200)]
        [NameAlias("eng", "knead {1} balls from the {0.Contents.}, and put them into {2}")]
        [NameAlias("hun", "gyúrj {1} dekás golyókat a(z) {0.Contents.K}, majd tedd őket a {2B}")]
        public void GolyovaGyurniC(IIngredientContainer c1, float weight, IIngredientContainer c2)
        {
            weight *= 10; // deka -> gramm

            IIngredient i = c1.Contents;

            if (!i.IsSolid) throw new InvalidActionForIngredientException("GolyovaGyurni", i);

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.PieceCount = count;

            LastActionDuration = 30 * (uint)count;

            c1.Empty();

            c2.Add(i);
        }

        [NameAlias("eng", "tear", Priority = 200)]
        [NameAlias("hun", "szaggat", Priority = 200)]
        [NameAlias("eng", "tear {1}-gramm pieces of the {0.Contents.}")]
        [NameAlias("hun", "szaggass {1} grammos darabokat a(z) {0.Contents.K}")]
        public IIngredient KiszaggatniC(IIngredientContainer c, float weight)
        {
            IIngredient i = c.Contents;

            if (!i.IsSolid) throw new InvalidActionForIngredientException("Kiszaggatni", i);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.PieceCount = count;

            LastActionDuration = 30 * (uint)count;

            c.Empty();
            return i;
        }

        [NameAlias("eng", "drip", Priority = 200)]
        [NameAlias("hun", "lecsepegtet", Priority = 200)]
        [NameAlias("eng", "drip the {0.Contents.}")]
        [NameAlias("hun", "csepegtesd le a(z) {0.Contents.T}")]
        public void LecsepegtetniC(IIngredientContainer c)
        {
            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "sprinkle", Priority = 200)]
        [NameAlias("hun", "rászór", Priority = 200)]
        [NameAlias("eng", "sprinkle the {1} onto the {0.Contents.}")]
        [NameAlias("hun", "szórd rá a(z) {1T} a(z) {0.Contents.R}")]
        public void Raszorni(IIngredientContainer iOn, IIngredient i)
        {
            if (!i.IsSolid) throw new InvalidActionForIngredientException("Raszorni", i);

            iOn.Add(i);

            this.LastActionDuration = 120;
        }

        [NameAlias("eng", "sprinkle", Priority = 200)]
        [NameAlias("hun", "rászór", Priority = 200)]
        [NameAlias("eng", "sprinkle the {1.Contents.} onto the {0.Contents.}")]
        [NameAlias("hun", "szórd rá a(z) {1.Contents.T} a(z) {0.Contents.R}")]
        public void RaszorniC(IIngredientContainer iOn, IIngredientContainer c)
        {
            IIngredient ci = c.Contents;

            if (!ci.IsSolid) throw new InvalidActionForIngredientException("Raszorni", ci);

            iOn.Add(ci);

            this.LastActionDuration = 120;
        }

        [NameAlias("eng", "sprinkle", Priority = 200)]
        [NameAlias("hun", "rászór", Priority = 200)]
        [NameAlias("eng", "sprinkle the {1} onto the {0.Contents.}")]
        [NameAlias("hun", "szórd rá a(z) {1T} a(z) {0.Contents.R}")]
        public void RaszorniI(IIngredientContainer iOn, ISingleIngredient i)
        {
            if (!i.IsSolid) throw new InvalidActionForIngredientException("Raszorni", i);

            iOn.Add(i);

            this.LastActionDuration = 120;
        }

        [NameAlias("eng", "superimpose", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("eng", "superimpose onto the {0} the following: ({1*}, )")]
        [NameAlias("hun", "rakd rá a(z) {0R} a következőket: ({1*}, )")]
        public void Rarakni(IIngredientContainer iOnTo, params IIngredient[] ingredients)
        {
            if (ingredients.Length == 0) return;

            foreach (IIngredient i in ingredients)
            {
                if (!i.IsSolid) throw new InvalidActionForIngredientException("Rarakni", i);
            }

            iOnTo.AddRange(ingredients);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "pour on", Priority = 200)]
        [NameAlias("hun", "ráönt", Priority = 200)]
        [NameAlias("hun", "öntsd rá a(z) {1T} a(z) {0.Contents.R}")]
        public void RaonteniI(IIngredientContainer iOnTo, IIngredient i)
        {
            if (!i.IsFluid) throw new InvalidActionForIngredientException("Raonteni", i);

            iOnTo.Add(i);

            this.LastActionDuration = 180;
        }

        [NameAlias("eng", "pour on", Priority = 200)]
        [NameAlias("hun", "ráönt", Priority = 200)]
        [NameAlias("eng", "pour on the {1.Contents.} onto the {0.Contents.}")]
        [NameAlias("hun", "öntsd rá a(z) {1.Contents.T} a(z) {0.Contents.R}")]
        public void RaonteniC(IIngredientContainer iOnTo, IIngredientContainer c)
        {
            IIngredient ci = c.Contents;

            if (!ci.IsFluid) throw new InvalidActionForIngredientException("Raonteni", ci);

            iOnTo.Add(ci);
            c.Empty();

            this.LastActionDuration = 180;
        }

        [NameAlias("eng", "water", Priority = 200)]
        [NameAlias("hun", "rálocsol", Priority = 200)]
        [NameAlias("eng", "water the {1.Contents.} onto the {0.Contents.}")]
        [NameAlias("hun", "locsold rá a(z) {1.Contents.T} a(z) {0.Contents.R}")]
        public void Ralocsolni(IIngredientContainer iOn, IIngredientContainer c)
        {
            if (!c.Contents.IsFluid) throw new InvalidActionForIngredientException("Lelocsolni", c.Contents);

            iOn.Add(c.Contents);
            c.Empty();

            this.LastActionDuration = 120;
        }

        [NameAlias("eng", "ted", Priority = 200)]
        [NameAlias("hun", "megforgat", Priority = 200)]
        [NameAlias("eng", "ted the {0} in the ({1*N}, )")]
        [NameAlias("hun", "forgasd meg a(z) {0T} a(z) ({1*N}, )")]
        public void MegforgatniI(ISingleIngredient i, params IIngredientContainer[] c)
        {
            if (!i.IsSolid) throw new InvalidActionForIngredientException("MegforgatniI", i);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "plow", Priority = 200)]
        [NameAlias("hun", "megforgat", Priority = 200)]
        [NameAlias("eng", "plow the {1.Contents.} in the {0}, and put it into the {2}")]
        [NameAlias("hun", "forgasd meg a(z) {1.Contents.T} a(z) {0N}, majd helyezd a {2B}")]
        public void MegforgatniC(IIngredientContainer iIn, IIngredientContainer c, IIngredientContainer c2)
        {
            IIngredient i = c.Contents;

            if (!i.IsSolid) throw new InvalidActionForIngredientException("Megforgatni", i);

            // TODO: a small amount of iIn.Contents must be separated

            this.LastActionDuration = 180;

            c2.AddRange(new IIngredient[] { i, iIn.Contents });
        }

        [NameAlias("eng", "water", Priority = 200)]
        [NameAlias("hun", "meglocsol", Priority = 200)]
        [NameAlias("eng", "water the {0.Contents.} with the {1}")]
        [NameAlias("hun", "locsold meg a(z) {0.Contents.T} a(z) {1V}")]
        public void MeglocsolniI(IIngredientContainer i, IIngredient iWith)
        {
            if (!iWith.IsFluid) throw new InvalidActionForIngredientException("Meglocsolni", iWith);

            this.LastActionDuration = 120;

            IIngredient iContents = i.Contents;
            i.Empty();
            i.AddRange(new IIngredient[] { iContents, iWith });
        }

        [NameAlias("eng", "water", Priority = 200)]
        [NameAlias("hun", "meglocsol", Priority = 200)]
        [NameAlias("eng", "water the {0.Contents.} with the {1}")]
        [NameAlias("hun", "locsold meg a(z) {0.Contents.T} a(z) {1V}")]
        public void MeglocsolniC(IIngredientContainer i, IIngredientContainer cWith)
        {
            IIngredient ci = cWith.Contents;

            if (!ci.IsFluid) throw new InvalidActionForIngredientException("Meglocsolni", ci);

            this.LastActionDuration = 120;

            i.Empty();
            i.AddRange(new IIngredient[] { ci, i.Contents });
        }

        [NameAlias("eng", "serve", Priority = 200)]
        [NameAlias("hun", "tálal", Priority = 200)]
        [NameAlias("eng", "serve the {0.Contents.} in the {1}")]
        [NameAlias("hun", "tálald a(z) {0.Contents.T} {1N}")]
        public void TalalniC(IIngredientContainer i, IEquipment container)
        {
            this.LastActionDuration = 300;
        }

        [NameAlias("eng", "separate", Priority = 200)]
        [NameAlias("hun", "szétválaszt", Priority = 200)]
        [NameAlias("eng", "separate the {0}")]
        [NameAlias("hun", "válaszd szét a(z) {0T}")]
        public IIngredient[] SzetvalasztaniI(IIngredient i)
        {
            if (!(i is Tojas)) throw new InvalidActionForIngredientException("Szetvalasztani", i);

            List<IIngredient> result = new List<IIngredient>();

            if (i is Tojas)
            {
                result.Add(new TojasSargaja(i.GetAmount()));
                result.Add(new TojasFeherje(i.GetAmount()));
                result[1].ChangeUnitTo(MeasurementUnit.liter);
            }

            this.LastActionDuration = 30 * (uint)i.PieceCount;

            return result.ToArray();
        }

        [NameAlias("eng", "roll", Priority = 200)]
        [NameAlias("hun", "felgöngyöl", Priority = 200)]
        [NameAlias("eng", "roll the {0.Contents.}")]
        [NameAlias("hun", "göngyöld fel a(z) {0.Contents.T}")]
        public void FelgongyolniC(IIngredientContainer c)
        {
            this.LastActionDuration = 240;
        }

        [NameAlias("eng", "dip into", Priority = 200)]
        [NameAlias("hun", "belemárt", Priority = 200)]
        [NameAlias("eng", "dip the {0.Contents.} into the {1.Contents.}")]
        [NameAlias("hun", "mártsd bele a(z) {0.Contents.T} a(z) {1.Contents.B}")]
        public void BelemartaniC(IIngredientContainer c1, IIngredientContainer c2)
        {
            IIngredient ci = c2.Contents;

            if (!ci.IsFluid) throw new InvalidActionForIngredientException("Belemartani", ci);

            // TODO: a small amount of iIn.Contents must be separated

            this.LastActionDuration = 30;
        }

        [NameAlias("eng", "wipe", Priority = 200)]
        [NameAlias("hun", "összemorzsol", Priority = 200)]
        [NameAlias("eng", "wipe the {0} into the {1}")]
        [NameAlias("hun", "morzsold össze a(z) {0T} a(z) {1B}")]
        public void Morzsol(ISingleIngredient i, IIngredientContainer c)
        {
            c.Add(i);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "wash", Priority = 200)]
        [NameAlias("hun", "megmos", Priority = 200)]
        [NameAlias("eng", "wash the {0} thoroughly")]
        [NameAlias("hun", "alaposan mosd meg a(z) {0T}")]
        public void MegmosniI(ISingleIngredient i)
        {
            if (!i.IsSolid) throw new InvalidActionForIngredientException("Megmosni", i);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "wrape around", Priority = 200)]
        [NameAlias("hun", "köréteker", Priority = 200)]
        [NameAlias("eng", "wrape the {0} around the {1}")]
        [NameAlias("hun", "tekerd a(z) {0T} a(z) {1} köré")]
        public void KoretekerniI(ISingleIngredient i1, ISingleIngredient i2)
        {
            if (!i1.IsSolid) throw new InvalidActionForIngredientException("KoretekerniI", i1);
            if (!i2.IsSolid) throw new InvalidActionForIngredientException("KoretekerniI", i2);

            this.LastActionDuration = 60;
        }
    }
}
