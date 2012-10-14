using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Ingredient;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Equipment
{
    [NameAlias("hun", "kéz")]

    public class Kez : Tool
    {
        [NameAlias("hun", "összegyúr", Priority = 200)]

        [NameAlias("hun", "gyúrd össze a következőket: ({0*}, )")]
        public IngredientGroup Osszegyurni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }

        [NameAlias("hun", "golyóvá gyúr", Priority = 200)]

        [NameAlias("hun", "gyúrd golyóvá a(z) {0T}")]
        public IIngredient GolyovaGyurni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("GolyovaGyurni", i.Name, i.Unit);
            
            // TODO: change dimensions and shape

            return i;
        }

        [NameAlias("hun", "szaggat", Priority = 200)]

        [NameAlias("hun", "szaggass {1} grammos darabokat a(z) {0B}")]
        public IngredientGroup Kiszaggatni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Kiszaggatni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                if (i is IngredientGroup)
                {
                    result.Add(((IngredientGroup)i).Clone(totalWeight / count, MeasurementUnit.gramm));
                }
                else
                {
                    result.Add(i.GetType().DefaultConstructor(totalWeight / count, MeasurementUnit.gramm) as IngredientBase);
                }

            }

            return new IngredientGroup(result.ToArray());
        }

        [NameAlias("hun", "lecsepegtet", Priority = 200)]

        [NameAlias("hun", "csepegtesd le a(z) {0T}")]
        public IIngredient Lecsepegtetni(IIngredient i) { return i; }

        [NameAlias("hun", "rászór", Priority = 200)]

        [NameAlias("hun", "szórd rá a(z) {1T} a(z) {0R}")]
        public IIngredient Raszorni(IIngredient iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Raszorni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOn, i });
        }


        [NameAlias("hun", "rárak", Priority = 200)]

        [NameAlias("hun", "rakd rá a(z) {1T} a(z) {0R}")]
        public IIngredient Rarakni(IIngredient iOnTo, IIngredient i)
        {
            if ((i.Unit != MeasurementUnit.piece) && (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Rarakni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOnTo, i });
        }

        [NameAlias("hun", "ráönt", Priority = 200)]

        [NameAlias("hun", "öntsd rá a(z) {1T} a(z) {0R}")]
        public IIngredient Raonteni(IIngredient iOnTo, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Raonteni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOnTo, i });
        }

        [NameAlias("hun", "rálocsol", Priority = 200)]

        [NameAlias("hun", "locsold rá a(z) {1T} a(z) {0R}")]
        public IIngredient Ralocsolni(IIngredient iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Lelocsolni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOn, i });
        }

        [NameAlias("hun", "megforgat", Priority = 200)]

        [NameAlias("hun", "forgasd meg a(z) {0T} a(z) {1N}")]
        public IIngredient Megforgatni(IIngredient i, IIngredient iIn)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Megforgatni", i.Name, i.Unit);

            // TODO: a small amount of i must be separated

            return new IngredientGroup(new IIngredient[] { i, iIn });
        }

        [NameAlias("hun", "tálal", Priority = 200)]

        [NameAlias("hun", "tálald a(z) {0T} a(z) {1N}")]
        public void Talalni(IIngredient i, IEquipment container) { }

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

            return result.ToArray();
        }
    }
}
