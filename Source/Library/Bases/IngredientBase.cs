using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library
{
    public class IngredientBase : IIngredient
    {
        public ShoppingListCategory Category { get; protected set; }

        private Dictionary<int, float> amounts = new Dictionary<int, float>();
        
        public MeasurementUnit Unit { get; set; }

        public IngredientState State { get; set; }

        public string Name
        {
            get
            {
                return GetName("hun");
            }
        }

        public int? ExpirationTime { get; private set; }
        public float? StorageTemperature { get; private set; }
        public int? GlichemicalIndex { get; private set; }
        public float? PotencialAlkalinity { get; private set; }

        public IngredientBase(float amount, MeasurementUnit unit, IngredientState state = IngredientState.Normal)
        {
            this.SetAmount(amount, unit);
            this.State = state;
        }

        public void LoadStaticInfoObject(object obj)
        {
            this.Category = (ShoppingListCategory)obj.GetType().GetProperty("Category").GetValue(obj, null);
            this.StorageTemperature = (float?)obj.GetType().GetProperty("StorageTemperature").GetValue(obj, null);
            this.Unit = (MeasurementUnit)obj.GetType().GetProperty("Unit").GetValue(obj, null);

            this.ExpirationTime = (int?)obj.GetType().GetProperty("ExpirationTime").GetValue(obj, null);
            this.GlichemicalIndex = (int?)obj.GetType().GetProperty("GlichemicalIndex").GetValue(obj, null);
            this.PotencialAlkalinity = (float?)obj.GetType().GetProperty("PotencialAlkalinity").GetValue(obj, null);
        }

        public float GetAmount()
        {
            return GetAmount(this.Unit);
        }

        public float GetAmount(MeasurementUnit unit)
        {
            float result;

            if (amounts.TryGetValue((int)unit, out result)) return result;

            throw new AmountUnknownException(this.GetType().Name, unit);
        }

        protected void SetAmount(float amount, MeasurementUnit unit)
        {
            if (amounts.ContainsKey((int)unit)) throw new AmountAlreadySetException(this.GetType().Name, unit);
            
            amounts.Add((int)unit, amount);
        }

        public string GetName(string culture)
        {
            string result = null;
            int priority = Int32.MaxValue;

            Attribute[] attributes = System.Attribute.GetCustomAttributes(this.GetType());
            foreach (Attribute attribute in attributes)
            {
                if (attribute is NameAliasAttribute)
                {
                    NameAliasAttribute naa = (NameAliasAttribute)attribute;
                    if ((naa.CultureName == culture) && (naa.Priority < priority)) result = naa.Name;
                }
            }

            if (result != null) return result;

            throw new CultureNotSupportedException(this.GetType().Name, culture);
        }

        public IIngredient Raszorni(IIngredient i)
        {
            if (i.Unit != MeasurementUnit.gramm) throw new InvalidActionForIngredientException("Raszorni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { this, i });
        }

        public IIngredient Rarakni(IIngredient i)
        {
            if (i.Unit != MeasurementUnit.piece) throw new InvalidActionForIngredientException("Rarakni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { this, i });
        }

        public IIngredient Lelocsolni(IIngredient i)
        {
            if (i.Unit != MeasurementUnit.liter) throw new InvalidActionForIngredientException("Lelocsolni", i.Name, i.Unit);

            return new IngredientGroup(new IIngredient[] { this, i });
        }

    }
}
