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

    public class IngredientBase : MarshalByRefObject, IIngredient
    {
        private static Dictionary<string,object> staticInfoObjects = new Dictionary<string,object>();

        private Dictionary<int, float> amounts = new Dictionary<int, float>();
        
        public MeasurementUnit Unit { get; protected set; }

        public IngredientState State { get; set; }

        public string Name
        {
            get
            {
                return NameAliasAttribute.GetDefaultName(this.GetType());
            }
        }

        public ShoppingListCategory? Category { get; protected set; }

        public int? ExpirationTime { get; protected set; }
        public float? StorageTemperature { get; protected set; }
        public int? GlichemicalIndex { get; protected set; }
        public float? PotencialAlkalinity { get; protected set; }

        public float? GrammsPerLiter { get; protected set; }
        public float? GrammsPerPiece { get; protected set; }
        public float? KCaloriesPerGramm { get; protected set; }

        public IngredientBase(float amount, MeasurementUnit unit, IngredientState state = IngredientState.Normal)
        {
            this.Unit = unit;
            this.SetAmount(amount, unit);
            this.State = state;

            foreach (object icaObj in this.GetType().GetCustomAttributes(typeof(IngredientConstsAttribute), true))
            {
                IngredientConstsAttribute ica = (IngredientConstsAttribute)icaObj;

                this.Category = ica.Category;
                
                this.ExpirationTime = ica.ExpirationTime;
                this.GlichemicalIndex = ica.GlichemicalIndex;
                this.PotencialAlkalinity = ica.PotencialAlkalinity;

                this.GrammsPerLiter = ica.GrammsPerLiter;
                this.GrammsPerPiece = ica.GrammsPerPiece;
                this.KCaloriesPerGramm = ica.KCaloriesPerGramm;
            }

            if (staticInfoObjects.ContainsKey(this.GetType().FullName)) LoadStaticInfoObject(staticInfoObjects[this.GetType().FullName]);
        }

        public void LoadStaticInfoObject(object obj)
        {
            if (obj == null) return;

            if (!staticInfoObjects.ContainsKey(this.GetType().FullName)) staticInfoObjects.Add(this.GetType().FullName, null);
            staticInfoObjects[this.GetType().FullName] = obj;

            try
            {
                // TODO: load only the properties which are NULL at the moment
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

        public void ChangeUnitTo(MeasurementUnit unit)
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

            SetAmount(amount, unit);

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

        public override string ToString()
        {
            string amountStr = "";

            try
            {
                float amount = GetAmount();

                switch (Unit)
                {
                    case MeasurementUnit.piece:
                        amountStr = (amount).ToString("0") + " db";
                        break;

                    case MeasurementUnit.portion:
                        amountStr = (amount).ToString("0") + " adag";
                        break;

                    case MeasurementUnit.gramm:
                        if (amount >= 1000) amountStr = (amount / 1000).ToString("0.0") + " kg";
                        else if (amount >= 100) amountStr = (amount / 10).ToString("0") + " dkg";
                        else if (amount >= 10) amountStr = (amount / 10).ToString("0.0") + " dkg";
                        else amountStr = (amount).ToString("0.00") + " g";
                        break;

                    case MeasurementUnit.liter:
                        if (amount >= 1) amountStr = (amount).ToString("0.0") + " l";
                        else if (amount >= 0.1) amountStr = (amount * 10).ToString("0") + " dl";
                        else if (amount >= 0.01) amountStr = (amount * 10).ToString("0.0") + " dl";
                        else amountStr = (amount * 100).ToString("0.00") + " cl";
                        break;

                    default:
                        break;
                }

                if (amount > 0) amountStr += " ";
            }
            catch (AmountUnknownException)
            { 
                // NOTE: that's fine for now, we do not calculate the amount for ingredient groups for the moment
            }

            return amountStr + Name;
        }
    }
}
