using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class IngredientBase : IIngredient
    {
        private object staticInfoObject;
        protected float? grammsPerPiece;

        public IngredientCategory Category { get; private set; }

        public float Amount { get; private set; }
        public MeasurementUnit Unit { get; set; }

        public int? ExpirationTime { get; private set; }
        public float? StorageTemperature { get; private set; }
        public int? GlichemicalIndex { get; private set; }
        public float? PotencialAlkalinity { get; private set; }
        public float? SpecificGravity { get; private set; }

        public IngredientBase(float amount, MeasurementUnit unit)
        {
            this.Amount = amount;
            this.Unit = unit;
        }

        public void LoadStaticInfoObject(object obj)
        {
            this.Category = (IngredientCategory)obj.GetType().GetProperty("Category").GetValue(obj, null);
            this.ExpirationTime = (int?)obj.GetType().GetProperty("ExpirationTime").GetValue(obj, null);
            this.StorageTemperature = (float?)obj.GetType().GetProperty("StorageTemperature").GetValue(obj, null);
            this.GlichemicalIndex = (int?)obj.GetType().GetProperty("GlichemicalIndex").GetValue(obj, null);
            this.PotencialAlkalinity = (float?)obj.GetType().GetProperty("PotencialAlkalinity").GetValue(obj, null);
            this.Unit = (MeasurementUnit)obj.GetType().GetProperty("Unit").GetValue(obj, null);
            this.SpecificGravity = (float?)obj.GetType().GetProperty("SpecificGravity").GetValue(obj, null);
        }
    }
}
