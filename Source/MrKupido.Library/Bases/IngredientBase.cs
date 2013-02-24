using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using System.Threading;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ingredient")]
    [NameAlias("hun", "hozzávaló")]

    public class IngredientBase : NamedObject, IIngredient
    {
        protected Dictionary<int, float> amounts = new Dictionary<int, float>();
        
        public MeasurementUnit Unit { get; protected set; }

        public float? GrammsPerLiter { get; protected set; }
        public float? GrammsPerPiece { get; protected set; }
        public float? KCaloriesPerGramm { get; protected set; }

        public IngredientState State { get; set; }

        public int PieceCount { get; set; }

        public IngredientBase(float amount, MeasurementUnit unit)
        {
            this.PieceCount = 1;
            this.Unit = unit;

            if (this.Unit == MeasurementUnit.piece)
            {
                amount = (float)Math.Ceiling((double)amount);
            }

            this.SetAmount(amount, unit);
        }

        public float GetAmount()
        {
            return GetAmount(this.Unit);
        }

        public float GetAmount(MeasurementUnit unit)
        {
            float result = 0.0f;

            if (amounts.TryGetValue((int)unit, out result)) return result;

            // generate the new unit
            MeasurementUnit mu = this.Unit;
            this.ChangeUnitTo(unit);
            this.Unit = mu;

            if (amounts.TryGetValue((int)unit, out result)) return result;

            throw new AmountUnknownException(this.GetType().Name, unit);
        }

        protected void SetAmount(float amount, MeasurementUnit unit)
        {
            if (amounts.ContainsKey((int)unit)) throw new AmountAlreadySetException(this.GetType().Name, unit);
            
            amounts.Add((int)unit, amount);
        }

        public virtual void ChangeUnitTo(MeasurementUnit unit)
        {
            if (this.Unit == unit) return;

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
                if (!this.GrammsPerLiter.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount /= this.GrammsPerLiter.Value;
            }

            if ((this.Unit == MeasurementUnit.liter) && (unit == MeasurementUnit.gramm))
            {
                if (!this.GrammsPerLiter.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount *= this.GrammsPerLiter.Value;
            }

            if ((this.Unit == MeasurementUnit.gramm) && (unit == MeasurementUnit.calories))
            {
                if (!this.KCaloriesPerGramm.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount *= this.KCaloriesPerGramm.Value;
            }

            if ((this.Unit == MeasurementUnit.calories) && (unit == MeasurementUnit.gramm))
            {
                if (!this.KCaloriesPerGramm.HasValue) throw new InvalidUnitConversionException(this, this.Unit, unit);

                amount /= this.KCaloriesPerGramm.Value;
            }

            if (!this.amounts.ContainsKey((int)unit))
            {
                SetAmount(amount, unit);
            }

            this.Unit = unit;
        }

        public void Add(IngredientBase operand)
        {
            if (this.Name != operand.Name) throw new MrKupidoException("The names of the ingredients must be the same for this operation! These are different: '{0}' and '{1}'.", this.Name, operand.Name);
            if (this.Unit != operand.Unit) throw new MrKupidoException("The units of the ingredients must be the same for this operation! These are different: '{0}' and '{1}'.", this.Unit, operand.Unit);

            float amount1 = this.GetAmount();
            float amount2 = operand.GetAmount();

            amounts.Clear();
            this.SetAmount(amount1 + amount2, this.Unit);
        }

        public virtual string ToString(string languageISO)
        {
            throw new NotImplementedException();
        }
    }
}
