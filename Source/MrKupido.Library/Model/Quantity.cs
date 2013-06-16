using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
	public class Quantity
	{
		protected Dictionary<int, float> amounts = new Dictionary<int, float>();

		public MeasurementUnit Unit { get; set; }

		private List<UnitConversionRule> ConversionRules { get; set; }

		private object masterClass;
		private Type masterType;

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

			new UnitConversionRule(MeasurementUnit.week, MeasurementUnit.day, "*", "7", true),
			new UnitConversionRule(MeasurementUnit.day, MeasurementUnit.hour, "*", "24", true),
			new UnitConversionRule(MeasurementUnit.hour, MeasurementUnit.minute, "*", "60", true),
			new UnitConversionRule(MeasurementUnit.minute, MeasurementUnit.second, "*", "60", true),

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

		private Quantity(object masterClass, float? amount, MeasurementUnit unit)
		{
			this.masterClass = masterClass;
			this.masterType = (masterClass == null ? null : masterClass.GetType());
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

			if (amount.HasValue)
			{
				SetAmount(amount.Value, unit);
				this.Unit = unit;
			}
		}

		public Quantity(object masterClass)
			: this(masterClass, null, MeasurementUnit.none)
		{
		}

		public Quantity(float amount, MeasurementUnit unit)
			: this(null, amount, unit)
		{
		}

		public void Clear()
		{
			amounts.Clear();
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

			throw new AmountUnknownException(masterType == null ? "(unknown)" : masterType.Name, unit);
		}

		public void SetAmount(float amount, MeasurementUnit unit)
		{
			if (amounts.ContainsKey((int)unit)) throw new AmountAlreadySetException(masterType == null ? "(unknown)" : masterType.Name, unit);

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
				if (!Single.TryParse(r.Parameter, System.Globalization.NumberStyles.Float, new System.Globalization.CultureInfo("en-US"), out parameterValue) && (masterType != null))
				{
					// we must look for a property
					System.Reflection.PropertyInfo pi = masterType.GetProperty(r.Parameter);
					if (pi == null)
					{
						Trace.TraceError("The class '{0}' does not support the conversion from unit '{1}' to '{2}', it does not have the needed property '{3}'.",
							masterType.Name, r.From, r.To, r.Parameter);
						return false;
					}

					if (masterClass != null)
					{
						float? propertyValue = (float?)pi.GetValue(masterClass, null);
						if (!propertyValue.HasValue)
						{
							Trace.TraceError("The class '{0}' does not support the conversion from unit '{1}' to '{2}'. If you want to support that, you need to set the '{3}' property of it with an attribute.",
								masterType.Name, r.From, r.To, r.Parameter);
							return false;
						}
						parameterValue = propertyValue.Value;
					}
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

		public override string ToString()
		{
			throw new MrKupidoException("Parameterless ToString() method of the Quantity is not supported. Use the ToString(string languageISO, MeasurementUnit displayUnit) instead.");
		}

		public string ToString(string languageISO)
		{
			return ToString(languageISO, this.Unit);
		}

		public string ToString(string languageISO, MeasurementUnit displayUnit)
		{
			string result = "";

			bool getNewAmount = true;
			float amount = 0.0f;
			UnitConstsAttribute uca = null;

			while (getNewAmount)
			{
				amount = GetAmount(displayUnit);
				getNewAmount = false;

				UnitConstsAttribute[] ucas = (UnitConstsAttribute[])displayUnit.GetType().GetField(displayUnit.ToString()).GetCustomAttributes(typeof(UnitConstsAttribute), false);
				if (ucas.Length > 0)
				{
					uca = ucas[0];

					if (amount < uca.SmallestAmount)
					{
						if (uca.UnitDown != MeasurementUnit.none)
						{
							displayUnit = uca.UnitDown;
							getNewAmount = true;
						}
						else
						{
							amount = uca.SmallestAmount;
						}
					}

					if (amount > uca.BiggestAmount)
					{
						if (uca.UnitUp != MeasurementUnit.none)
						{
							displayUnit = uca.UnitUp;
							getNewAmount = true;
						}
						else
						{
							amount = uca.BiggestAmount;
						}
					}

					int counter = (int)(amount / uca.SmallestAmount);
					if ((amount / uca.SmallestAmount) != counter)
					{
						amount = (counter + 1) * uca.SmallestAmount;
					}
				}
			}

			if (amount > 0)
			{
				result += amount.ToString() + " " + NameAliasAttribute.GetName(languageISO, typeof(MeasurementUnit), displayUnit.ToString());

				if (this.masterClass != null) result += " ";
			}

			return result;
		}
	}
}
