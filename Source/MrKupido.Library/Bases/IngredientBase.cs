using MrKupido.Library.Attributes;
using System;
using System.Linq;
using System.Reflection;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ingredient")]
    [NameAlias("hun", "hozzávaló")]

    public class IngredientBase : NamedObject, IIngredient
    {
        public MeasurementUnit PreferredUnit { get; private set; }

        public MeasurementUnit Unit
        {
            get
            {
                return Quantity.Unit;
            }

            protected set
            {
                Quantity.Unit = value;
            }
        }

        protected Quantity Quantity { get; private set; }

        public float? GrammsPerLiter { get; protected set; }
        public float? GrammsPerPiece { get; protected set; }
        public float? CaloriesPer100Gramms { get; protected set; }
        public float? CarbohydratesPer100Gramms { get; protected set; }
        public float? ProteinPer100Gramms { get; protected set; }
        public float? FatPer100Gramms { get; protected set; }

        public IngredientState State { get; set; }

        public int PieceCount { get; set; }

        public bool IsSolid
        {
            get
            {
                MeasurementUnit unitToCheck = this.Unit;

                if ((unitToCheck == MeasurementUnit.bogre) || (unitToCheck == MeasurementUnit.csesze) || (unitToCheck == MeasurementUnit.evokanal) || (unitToCheck == MeasurementUnit.gyermekkanal)
                    || (unitToCheck == MeasurementUnit.kaveskanal) || (unitToCheck == MeasurementUnit.mokkaskanal) || (unitToCheck == MeasurementUnit.teaskanal))
                {
                    foreach (ConstructorInfo ci in this.GetType().GetConstructors())
                    {
                        ParameterInfo pi = ci.GetParameters().FirstOrDefault(p => p.IsOptional && (p.ParameterType == typeof(MeasurementUnit)));
                        if (pi != null)
                        {
                            unitToCheck = (MeasurementUnit)pi.DefaultValue;
                            break;
                        }
                    }
                }

                return (
                (unitToCheck == MeasurementUnit.milligramm) || (unitToCheck == MeasurementUnit.gramm) || (unitToCheck == MeasurementUnit.dekagramm) || (unitToCheck == MeasurementUnit.kilogramm)
                || (unitToCheck == MeasurementUnit.piece) || (unitToCheck == MeasurementUnit.csipet));
            }
        }

        public bool IsFluid
        {
            get
            {
                MeasurementUnit unitToCheck = this.Unit;

                if ((unitToCheck == MeasurementUnit.bogre) || (unitToCheck == MeasurementUnit.csesze) || (unitToCheck == MeasurementUnit.evokanal) || (unitToCheck == MeasurementUnit.gyermekkanal)
                    || (unitToCheck == MeasurementUnit.kaveskanal) || (unitToCheck == MeasurementUnit.mokkaskanal) || (unitToCheck == MeasurementUnit.teaskanal))
                {
                    foreach (ConstructorInfo ci in this.GetType().GetConstructors())
                    {
                        ParameterInfo pi = ci.GetParameters().FirstOrDefault(p => p.IsOptional && (p.ParameterType == typeof(MeasurementUnit)));
                        if (pi != null)
                        {
                            unitToCheck = (MeasurementUnit)pi.DefaultValue;
                            break;
                        }
                    }
                }

                return ((unitToCheck == MeasurementUnit.milliliter) || (unitToCheck == MeasurementUnit.centiliter) || (unitToCheck == MeasurementUnit.deciliter) || (unitToCheck == MeasurementUnit.liter));
            }
        }

        public IngredientBase(float amount, MeasurementUnit unit)
        {
            this.PieceCount = 1;
            this.Quantity = new Quantity(this);
            this.Unit = unit;
            this.PreferredUnit = unit;

            if ((this.Unit == MeasurementUnit.piece) || (this.Unit == MeasurementUnit.csipet))
            {
                amount = (float)Math.Ceiling((double)amount);
            }

            this.SetAmount(amount, unit);
        }

        public float GetAmount()
        {
            return Quantity.GetAmount();
        }

        public float GetAmount(MeasurementUnit unit)
        {
            return Quantity.GetAmount(unit);
        }

        protected void SetAmount(float amount, MeasurementUnit unit)
        {
            Quantity.SetAmount(amount, unit);
        }

        public virtual bool ChangeUnitTo(MeasurementUnit unit)
        {
            return Quantity.ChangeUnitTo(unit);
        }

        public void Add(IngredientBase operand)
        {
            if (this.Name != operand.Name) throw new MrKupidoException("The names of the ingredients must be the same for this operation! These are different: '{0}' and '{1}'.", this.Name, operand.Name);
            if (this.Unit != operand.Unit) throw new MrKupidoException("The units of the ingredients must be the same for this operation! These are different: '{0}' and '{1}'.", this.Unit, operand.Unit);

            float amount1 = this.GetAmount();
            float amount2 = operand.GetAmount();

            Quantity.Clear();
            this.SetAmount(amount1 + amount2, this.Unit);
        }

        public virtual string ToString(string languageISO)
        {
            throw new NotImplementedException();
        }
    }
}
