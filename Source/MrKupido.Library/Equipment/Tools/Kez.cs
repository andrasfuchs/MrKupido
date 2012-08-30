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
        public IngredientGroup Osszegyurni(params IIngredient[] ingredients)
        {
            return new IngredientGroup(ingredients);
        }

        public IIngredient GolyovaGyurni(IIngredient i)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("GolyovaGyurni", i.Name, i.Unit);
            
            // TODO: change dimensions and shape

            return i;
        }

        public IngredientGroup Kiszaggatni(IIngredient i, float weight)
        {
            if ((!(i is IngredientBase)) || (i.Unit != MeasurementUnit.gramm)) throw new InvalidActionForIngredientException("Kiszaggatni", i.Name, i.Unit);

            List<IIngredient> result = new List<IIngredient>();

            float totalWeight = i.GetAmount(MeasurementUnit.gramm);
            int count = ((int)Math.Floor(totalWeight / weight)) + 1;

            for (int j = 0; j < count; j++)
            {
                result.Add((IIngredient)Activator.CreateInstance(i.GetType(), totalWeight / count, MeasurementUnit.gramm));
            }

            return new IngredientGroup(result.ToArray());
        }

        public IIngredient Lecsepegtetni(IIngredient i) { return i; }

        public IIngredient Raszorni(IIngredient iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Raszorni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOn, i });
        }

        public IIngredient Rarakni(IIngredient iOnTo, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.piece) throw new InvalidActionForIngredientException("Rarakni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOnTo, i });
        }

        public IIngredient Ralocsolni(IIngredient iOn, IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Lelocsolni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { iOn, i });
        }

        public IIngredient Megforgatni(IIngredient i, IIngredient iIn)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Megforgatni", i.Name, i.Unit);

            // TODO: a small amount of i must be separated

            return new IngredientGroup(new IIngredient[] { iIn, i });
        }

        public void Talalni(IIngredient i, IEquipment container) { }
    }
}
