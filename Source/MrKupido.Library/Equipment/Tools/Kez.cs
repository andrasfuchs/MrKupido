using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("eng", "hand")]
    [NameAlias("hun", "kéz")]
    public class Kez : Tool
    {
        [NameAlias("eng", "crumple", Priority = 200)]
        [NameAlias("hun", "összegyúr", Priority = 200)]
        [NameAlias("hun", "gyúrd össze a következőket: ({0*}, )")]
        public IngredientGroup Osszegyurni(params IIngredient[] ingredients)
        {
            this.LastActionDuration = 180;

            return new IngredientGroup(ingredients);
        }

        [NameAlias("eng", "crumple", Priority = 200)]
        [NameAlias("hun", "összegyúr", Priority = 200)]
        [NameAlias("hun", "gyúrd össze a(z) {0.Contents.T}")]
        public void OsszegyurniEdenyben(IIngredientContainer container)
        {
            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "knead to balls", Priority = 200)]
        [NameAlias("hun", "golyóvá gyúr", Priority = 200)]
        [NameAlias("hun", "gyúrd golyóvá a(z) {0T}")]
        public IngredientGroup GolyovaGyurni(IngredientGroup ig)
        {
            if (ig.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("GolyovaGyurni", ig.Name, ig.Unit);
            
            // TODO: change dimensions and shape

            this.LastActionDuration = 60;

            return ig;
        }

        [NameAlias("eng", "tear", Priority = 200)]
        [NameAlias("hun", "szaggat", Priority = 200)]
        [NameAlias("hun", "szaggass {1} grammos darabokat a(z) {0K}")]
        public IngredientGroup Kiszaggatni(IIngredient i, float weight)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Kiszaggatni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            i.PieceCount = count;

            LastActionDuration = 30 * (uint)count;

            return new IngredientGroup(i);
        }

        [NameAlias("eng", "drip", Priority = 200)]
        [NameAlias("hun", "lecsepegtet", Priority = 200)]
        [NameAlias("hun", "csepegtesd le a(z) {0T}")]
        public IIngredient Lecsepegtetni(IIngredient i) 
        {
            this.LastActionDuration = 60;

            return i; 
        }

        [NameAlias("eng", "sprinkle", Priority = 200)]
        [NameAlias("hun", "rászór", Priority = 200)]
        [NameAlias("hun", "szórd rá a(z) {1T} a(z) {0.Contents.R}")]
        public void Raszorni(IIngredientContainer iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Raszorni", i.Name, i.Unit);

            iOn.Add(i);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "superimpose", Priority = 200)]
        [NameAlias("hun", "rárak", Priority = 200)]
        [NameAlias("hun", "rakd rá a(z) {0R} a következőket: ({1*}, )")]
        public void Rarakni(IIngredientContainer iOnTo, params IIngredient[] ingredients)
        {
            if (ingredients.Length == 0) return;

            foreach (IIngredient i in ingredients)
            {
                if ((i.Unit != MeasurementUnit.piece) && (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Rarakni", i.Name, i.Unit);
            }

            iOnTo.AddRange(ingredients);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "pour on", Priority = 200)]
        [NameAlias("hun", "ráönt", Priority = 200)]
        [NameAlias("hun", "öntsd rá a(z) {1T} a(z) {0.Contents.R}")]
        public void Raonteni(IIngredientContainer iOnTo, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Raonteni", i.Name, i.Unit);

            iOnTo.Add(i);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "water", Priority = 200)]
        [NameAlias("hun", "rálocsol", Priority = 200)]
        [NameAlias("hun", "locsold rá a(z) {1T} a(z) {0.Contents.R}")]
        public void Ralocsolni(IIngredientContainer iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Lelocsolni", i.Name, i.Unit);

            iOn.Add(i);

            this.LastActionDuration = 60;
        }

        [NameAlias("eng", "plow", Priority = 200)]
        [NameAlias("hun", "megforgat", Priority = 200)]
        [NameAlias("hun", "forgasd meg a(z) {1T} a(z) {0N}")]
        public IngredientGroup Megforgatni(IIngredientContainer iIn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Megforgatni", i.Name, i.Unit);

            // TODO: a small amount of iIn.Contents must be separated

            this.LastActionDuration = 180;

            return new IngredientGroup(new IIngredient[] { i, iIn.Contents });
        }

        [NameAlias("eng", "serve", Priority = 200)]
        [NameAlias("hun", "tálal", Priority = 200)]
        [NameAlias("hun", "tálald a(z) {0.Contents.T} {1N}")]
        public void Talalni(IIngredientContainer i, IEquipment container) 
        {
            this.LastActionDuration = 300;
        }

        [NameAlias("eng", "separate", Priority = 200)]
        [NameAlias("hun", "szétválaszt", Priority = 200)]
        [NameAlias("hun", "válaszd szét a(z) {0T}")]
        public IIngredient[] Szetvalasztani(IIngredient i) 
        {
            if (!(i is Tojas)) throw new InvalidActionForIngredientException("Szetvalasztani", i.Name, i.Unit);

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
    }
}
