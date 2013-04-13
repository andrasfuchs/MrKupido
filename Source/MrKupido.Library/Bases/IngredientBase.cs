using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;
using System.Threading;
using System.Diagnostics;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "ingredient")]
    [NameAlias("hun", "hozzávaló")]

    public class IngredientBase : NamedObject, IIngredient
    {
        protected Dictionary<int, float> amounts = new Dictionary<int, float>();
        
        public MeasurementUnit Unit { get; protected set; }
		public MeasurementUnit PreferredUnit { get; private set; }

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
				return (
				(this.Unit == MeasurementUnit.milligramm) || (this.Unit == MeasurementUnit.gramm) || (this.Unit == MeasurementUnit.dekagramm) || (this.Unit == MeasurementUnit.kilogramm)
				|| (this.Unit == MeasurementUnit.piece) || (this.Unit == MeasurementUnit.csipet));
			}
		}

		public bool IsFluid
		{
			get
			{
				return ((this.Unit == MeasurementUnit.milliliter) || (this.Unit == MeasurementUnit.centiliter) || (this.Unit == MeasurementUnit.deciliter) || (this.Unit == MeasurementUnit.liter));
			}
		}	


		private List<UnitConversionRule> ConversionRules { get; set; }

		private static UnitConversionRule[] conversionRules = 
		{ 
			new UnitConversionRule(MeasurementUnit.kilogramm, MeasurementUnit.gramm, "*", "1000", true),
			new UnitConversionRule(MeasurementUnit.dekagramm, MeasurementUnit.gramm, "*", "10", true),
			new UnitConversionRule(MeasurementUnit.csipet, MeasurementUnit.gramm, "*", "1.5", true),
			new UnitConversionRule(MeasurementUnit.gramm, MeasurementUnit.milligramm, "*", "1000", true),

			new UnitConversionRule(MeasurementUnit.liter, MeasurementUnit.deciliter, "*", "10", true),
			new UnitConversionRule(MeasurementUnit.liter, MeasurementUnit.centiliter, "*", "100", true),
			new UnitConversionRule(MeasurementUnit.liter, MeasurementUnit.milliliter, "*", "1000", true),

			new UnitConversionRule(MeasurementUnit.meter, MeasurementUnit.decimeter, "*", "10", true),
			new UnitConversionRule(MeasurementUnit.meter, MeasurementUnit.centimeter, "*", "100", true),
			new UnitConversionRule(MeasurementUnit.meter, MeasurementUnit.millimeter, "*", "1000", true),
			new UnitConversionRule(MeasurementUnit.meter, MeasurementUnit.folyometer, "*", "1", true),

			new UnitConversionRule(MeasurementUnit.mokkaskanal, MeasurementUnit.milliliter, "*", "2", true),
			new UnitConversionRule(MeasurementUnit.kaveskanal, MeasurementUnit.milliliter, "*", "5", true),
			new UnitConversionRule(MeasurementUnit.teaskanal, MeasurementUnit.milliliter, "*", "5", true),
			new UnitConversionRule(MeasurementUnit.gyermekkanal, MeasurementUnit.milliliter, "*", "10", true),
			new UnitConversionRule(MeasurementUnit.evokanal, MeasurementUnit.milliliter, "*", "15", true),

			new UnitConversionRule(MeasurementUnit.pohar, MeasurementUnit.deciliter, "*", "2", true),
			new UnitConversionRule(MeasurementUnit.csesze, MeasurementUnit.deciliter, "*", "2.5", true),
			new UnitConversionRule(MeasurementUnit.bogre, MeasurementUnit.deciliter, "*", "5", true),

			new UnitConversionRule(MeasurementUnit.piece, MeasurementUnit.gramm, "*", "GrammsPerPiece", true),
			new UnitConversionRule(MeasurementUnit.liter, MeasurementUnit.gramm, "*", "GrammsPerLiter", true),
			new UnitConversionRule(MeasurementUnit.gramm, MeasurementUnit.calories, "*", "CaloriesPer100Gramms", true),
		};

        public IngredientBase(float amount, MeasurementUnit unit)
        {
            this.PieceCount = 1;
            this.Unit = unit;
			this.PreferredUnit = unit;

            if ((this.Unit == MeasurementUnit.piece) || (this.Unit == MeasurementUnit.csipet))
            {
                amount = (float)Math.Ceiling((double)amount);
            }

            this.SetAmount(amount, unit);			

			this.ConversionRules = new List<UnitConversionRule>();
			foreach (UnitConversionRule ucr in conversionRules)
			{
				ConversionRules.Add(new UnitConversionRule(ucr.From, ucr.To, ucr.Command, ucr.Parameter, false));

				if (ucr.Reversable)
				{
					string revCommand = "N/A";

					switch (ucr.Command)
					{
						case "*":
							revCommand = "/";
							break;

						case "/":
							revCommand = "*";
							break;

						default:
							Trace.TraceError("The unit conversion rule has a command '{0}' which can't be reversed.", ucr.Command);
							break;
					}

					ConversionRules.Add(new UnitConversionRule(ucr.To, ucr.From, revCommand, ucr.Parameter, false));
				}
			}
        }

        public float GetAmount()
        {
            return GetAmount(this.Unit);
        }

        public float GetAmount(MeasurementUnit unit)
        {
            float result = 0.0f;

			MeasurementUnit originalUnit = this.Unit;
            this.ChangeUnitTo(unit); // this one changes the current unit to the 'unit' value
			this.Unit = originalUnit;
            
			if (amounts.TryGetValue((int)unit, out result)) return result;

            throw new AmountUnknownException(this.GetType().Name, unit);
        }

        protected void SetAmount(float amount, MeasurementUnit unit)
        {
            if (amounts.ContainsKey((int)unit)) throw new AmountAlreadySetException(this.GetType().Name, unit);
            
            amounts.Add((int)unit, amount);
        }

        public virtual bool ChangeUnitTo(MeasurementUnit unit)
        {
            if (this.Unit == unit) return true;

            float amount = GetAmount(this.Unit);

			List<UnitConversionRule> rules = new List<UnitConversionRule>();
			FindRulePath(this.Unit, unit, rules);

			if (rules.Count == 0)
			{
				Trace.TraceError("The conversion from unit '{0}' to '{1}' is not suppoted at the moment. If you need to use it, extend the 'conversionRules' array.", this.Unit, unit);
				return false;
			}

			for (int i = 0; i < rules.Count; i++)
			{
				UnitConversionRule r = rules[i];

				float parameterValue = 0.0f;
				if (!Single.TryParse(r.Parameter, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"), out parameterValue))
				{
					// we must look for a property
					System.Reflection.PropertyInfo pi = this.GetType().GetProperty(r.Parameter);
					if (pi == null)
					{
						Trace.TraceError("The class '{0}' does not support the conversion from unit '{1}' to '{2}', it does not have the needed property '{3}'.",
							this.GetType().Name, r.From, r.To, r.Parameter);
						return false;
					}

					float? propertyValue = (float?)pi.GetValue(this, null);
					if (!propertyValue.HasValue)
					{
						Trace.TraceError("The class '{0}' does not support the conversion from unit '{1}' to '{2}'. If you want to support that, you need to set the '{3}' property of it with an attribute.",
							this.GetType().Name, r.From, r.To, r.Parameter);
						return false;
					}

					parameterValue = propertyValue.Value;
				}


				switch (r.Command)
				{
					case "*":
						amount *= parameterValue;
						break;

					case "/":
						amount /= parameterValue;
						break;

					default:
						Trace.TraceError("The unit conversion rule from unit '{0}' to '{1}' has an unknown command '{2}'.", r.From, r.To, r.Command);
						return false;
				}

				// 

				if (!this.amounts.ContainsKey((int)r.To))
				{
					SetAmount(amount, r.To);
				}

				this.Unit = r.To;
			}

			return true;
        }

		private bool FindRulePath(MeasurementUnit from, MeasurementUnit to, List<UnitConversionRule> ruleList)
		{
			List<UnitConversionRule> result = new List<UnitConversionRule>();

			UnitConversionRule r = ConversionRules.FirstOrDefault(ucr => (ucr.From == from) && (ucr.To == to) && (!ruleList.Contains(ucr)));

			if (r == null)
			{
				foreach (UnitConversionRule ucr in ConversionRules.Where(cr => (cr.From == from) && (!ruleList.Contains(cr))))	
				{
					ruleList.Add(ucr);
					if (FindRulePath(ucr.To, to, ruleList))
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				ruleList.Add(r);
				return true;
			}
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
