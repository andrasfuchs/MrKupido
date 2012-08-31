﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using System.Threading;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("hun", "hozzávalók")]
    [NameAlias("eng", "ingredients")]

    public class IngredientBase : IIngredient
    {
        private static Dictionary<string,object> staticInfoObjects = new Dictionary<string,object>();

        public ShoppingListCategory Category { get; protected set; }

        private Dictionary<int, float> amounts = new Dictionary<int, float>();
        
        public MeasurementUnit Unit { get; set; }

        public IngredientState State { get; set; }

        public string Name
        {
            get
            {
                return NameAliasAttribute.GetDefaultName(this.GetType());
            }
        }

        public int? ExpirationTime { get; private set; }
        public float? StorageTemperature { get; private set; }
        public int? GlichemicalIndex { get; private set; }
        public float? PotencialAlkalinity { get; private set; }

        public float? GrammsPerLiter { get; private set; }
        public float? GrammsPerPiece { get; private set; }
        public float? KCaloriesPerGramm { get; private set; }

        public IngredientBase(float amount, MeasurementUnit unit, IngredientState state = IngredientState.Normal)
        {
            this.Unit = unit;
            this.SetAmount(amount, unit);
            this.State = state;

            if (staticInfoObjects.ContainsKey(this.GetType().FullName)) LoadStaticInfoObject(staticInfoObjects[this.GetType().FullName]);
        }

        public void LoadStaticInfoObject(object obj)
        {
            if (obj == null) return;

            if (!staticInfoObjects.ContainsKey(this.GetType().FullName)) staticInfoObjects.Add(this.GetType().FullName, null);
            staticInfoObjects[this.GetType().FullName] = obj;

            try
            {
                this.Category = (ShoppingListCategory)obj.GetType().GetProperty("Category").GetValue(obj, null);
                this.StorageTemperature = (float?)obj.GetType().GetProperty("StorageTemperature").GetValue(obj, null);
                //this.Unit = (MeasurementUnit)obj.GetType().GetProperty("Unit").GetValue(obj, null);

                this.ExpirationTime = (int?)obj.GetType().GetProperty("ExpirationTime").GetValue(obj, null);
                this.GlichemicalIndex = (int?)obj.GetType().GetProperty("GlichemicalIndex").GetValue(obj, null);
                this.PotencialAlkalinity = (float?)obj.GetType().GetProperty("PotencialAlkalinity").GetValue(obj, null);

                this.GrammsPerLiter = (float?)obj.GetType().GetProperty("GrammsPerLiter").GetValue(obj, null);
                this.GrammsPerPiece = (float?)obj.GetType().GetProperty("GrammsPerPiece").GetValue(obj, null);
                this.KCaloriesPerGramm = (float?)obj.GetType().GetProperty("KCaloriesPerGramm").GetValue(obj, null);
            }
            catch (Exception ex)
            {
                throw new MrKupidoException(ex, "This object '{0}' can not be used as the static info object of type '{1}'. It doesn't have the necessary properties implemented or they are not accessible.", obj.GetType().FullName, this.GetType().FullName);
            }
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

        public void ChangeUnitTo(MeasurementUnit unit)
        {
            float amount = GetAmount(this.Unit);

            if ((this.Unit == MeasurementUnit.gramm) && (unit == MeasurementUnit.piece))
            {
                if (!this.GrammsPerPiece.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount /= this.GrammsPerPiece.Value;
            }

            if ((this.Unit == MeasurementUnit.piece) && (unit == MeasurementUnit.gramm))
            {
                if (!this.GrammsPerPiece.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount *= this.GrammsPerPiece.Value;
            }

            if ((this.Unit == MeasurementUnit.gramm) && (unit == MeasurementUnit.liter))
            {
                if (!this.GrammsPerPiece.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount /= this.GrammsPerLiter.Value;
            }

            if ((this.Unit == MeasurementUnit.liter) && (unit == MeasurementUnit.gramm))
            {
                if (!this.GrammsPerPiece.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount *= this.GrammsPerLiter.Value;
            }

            if ((this.Unit == MeasurementUnit.gramm) && (unit == MeasurementUnit.calories))
            {
                if (!this.GrammsPerPiece.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount *= this.KCaloriesPerGramm.Value;
            }

            if ((this.Unit == MeasurementUnit.calories) && (unit == MeasurementUnit.gramm))
            {
                if (!this.GrammsPerPiece.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount /= this.KCaloriesPerGramm.Value;
            }

            SetAmount(amount, unit);

            this.Unit = unit;
        }
    }
}
