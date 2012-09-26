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
        [NameAlias("hun", "gyúrd össze a következőket: ({0*}, )")]
        public IngredientGroup Osszegyurni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }

        [NameAlias("hun", "gyúrd golyóvá a(z) {0T}")]
        public IIngredient GolyovaGyurni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("GolyovaGyurni", i.Name, i.Unit);
            
            // TODO: change dimensions and shape

            return i;
        }

        [NameAlias("hun", "szaggass {1} grammos darabokat a(z) {0B}")]
        public IngredientGroup Kiszaggatni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Kiszaggatni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                result.Add(i.GetType().DefaultConstructor(totalWeight / count, MeasurementUnit.gramm) as IngredientBase);
            }

            return new IngredientGroup(result.ToArray());
        }

        [NameAlias("hun", "csepegtesd le a {0T}")]
        public IIngredient Lecsepegtetni(IIngredient i) { return i; }

        [NameAlias("hun", "szórd rá a(z) {1T} a(z) {0R}")]
        public IIngredient Raszorni(IIngredient iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Raszorni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOn, i });
        }

        [NameAlias("hun", "rakd rá a(z) {1T} a(z) {0R}")]
        public IIngredient Rarakni(IIngredient iOnTo, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.piece) throw new InvalidActionForIngredientException("Rarakni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOnTo, i });
        }

        [NameAlias("hun", "locsold rá a(z) {1T} a(z) {0R}")]
        public IIngredient Ralocsolni(IIngredient iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Lelocsolni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOn, i });
        }

        [NameAlias("hun", "forgasd meg a(z) {0T} a(z) {1N}")]
        public IIngredient Megforgatni(IIngredient i, IIngredient iIn)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Megforgatni", i.Name, i.Unit);

            // TODO: a small amount of i must be separated

            return new IngredientGroup(new IIngredient[] { i, iIn });
        }

        [NameAlias("hun", "tálald a(z) {0T} a(z) {1N}")]
        public void Talalni(IIngredient i, IEquipment container) { }
    }
}
