using MrKupido.Library.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Ingredient
{
    [NameAlias("eng", "single ingredient")]
    [NameAlias("hun", "egyszerű hozzávaló")]

    public class SingleIngredient : IngredientBase, ISingleIngredient
    {
        public ShoppingListCategory? Category { get; protected set; }

        public int? ExpirationTime { get; protected set; }
        public float? StorageTemperature { get; protected set; }
        public int? GlichemicalIndex { get; protected set; }
        public float? PotencialAlkalinity { get; protected set; }

        public SingleIngredient(float amount, MeasurementUnit unit, IngredientState state = IngredientState.Normal) : base(amount, unit)
        {
            this.State = state;
            this.PieceCount = 1;

            foreach (object icaObj in this.GetType().GetCustomAttributes(typeof(IngredientConstsAttribute), false))
            {
                IngredientConstsAttribute ica = (IngredientConstsAttribute)icaObj;

				while (ica != null)
				{
					CopyFieldsToPropertiesIfNeeded(this, ica, new string[] { "Category", "ExpirationTime", "GlichemicalIndex", "PotencialAlkalinity", "GrammsPerLiter", "GrammsPerPiece", "CaloriesPer100Gramms", "CarbohydratesPer100Gramms", "ProteinPer100Gramms", "FatPer100Gramms" });

					if (ica.DefaultChild != null)
					{
						ica = (IngredientConstsAttribute)ica.DefaultChild.GetCustomAttributes(typeof(IngredientConstsAttribute), false).FirstOrDefault();
					}
					else
					{
						break;
					}
				}

				//this.Category = ica.Category;
                
				//this.ExpirationTime = ica.ExpirationTime;
				//this.GlichemicalIndex = ica.GlichemicalIndex;
				//this.PotencialAlkalinity = ica.PotencialAlkalinity;

				//this.GrammsPerLiter = this.GrammsPerLiter == null && ica.GrammsPerLiter != Single.MinValue ? ica.GrammsPerLiter : (float?)null;
				//this.GrammsPerPiece = ica.GrammsPerPiece == Single.MinValue ? (float?)null : ica.GrammsPerPiece;
				//this.CaloriesPer100Gramm = ica.CaloriesPer100Gramms == Single.MinValue ? (float?)null : ica.CaloriesPer100Gramms;
				//this.CarbohydratesPer100Gramms = ica.CarbohydratesPer100Gramms == Single.MinValue ? (float?)null : ica.CarbohydratesPer100Gramms;
				//this.ProteinPer100Gramms = ica.ProteinPer100Gramms == Single.MinValue ? (float?)null : ica.ProteinPer100Gramms;
				//this.FatPer100Gramms = ica.FatPer100Gramms == Single.MinValue ? (float?)null : ica.FatPer100Gramms;
            }
        }

		private void CopyFieldsToPropertiesIfNeeded(object o1, object o2, string[] propNames)
		{
			foreach (string propName in propNames)
			{
				System.Reflection.PropertyInfo o1pi = o1.GetType().GetProperty(propName);

				object o1Value = o1pi.GetValue(o1, null);
				object o2Value = o2.GetType().GetField(propName).GetValue(o2);

				if ((o1Value == null) && (o2Value != null))
				{
					// check if it's minValue
					object minValue = null;
					bool isO2ValueEqualsToMinValue = false;

					System.Reflection.FieldInfo fi = o2Value.GetType().GetField("MinValue");
					if (fi != null)
					{
						minValue = fi.GetValue(o2Value);
						isO2ValueEqualsToMinValue = (bool)o2Value.GetType().GetMethod("Equals", new Type[] { o2Value.GetType() }).Invoke(o2Value, new object[] { minValue });
					}

					if (!isO2ValueEqualsToMinValue)
					{
						// if it's not null and not minValue (this one is needed because Attributes can't have nullable optional parameters)
						o1pi.SetValue(o1, o2Value, null);
					}
				}
			}
		}

        public override string ToString(string languageISO)
        {
            return ToString(languageISO, true, true);
        }

        public string ToString(string languageISO, bool includeAmount, bool includeState)
        {
            string amountStr = "";

            if (includeAmount)
            {
                try
                {
                    float amount = GetAmount();

                    if (amount > 0)
                    {

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

                        amountStr += " ";
                    }
                }
                catch (AmountUnknownException)
                {
                    // NOTE: that's fine for now, we do not calculate the amount for ingredient groups for the moment
                }
            }

            string stateStr = "";

            if (includeState)
            {
                if (State != IngredientState.Normal)
                {
                    stateStr = new EnumToMultilingualString().ConvertToString(State) + " ";
                }
            }

            return amountStr + stateStr + this.GetName(languageISO);
        }
    }
}
