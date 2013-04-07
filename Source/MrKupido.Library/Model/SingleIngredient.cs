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

		public float? PieceCountEstimation { get; private set; }
		public float? StandardPortionCalories { get; protected set; }

        public SingleIngredient(float amount, MeasurementUnit unit, IngredientState state = IngredientState.Normal) : base(amount, unit)
        {
            this.State = state;

            foreach (object icaObj in this.GetType().GetCustomAttributes(typeof(IngredientConstsAttribute), false))
            {
                IngredientConstsAttribute ica = (IngredientConstsAttribute)icaObj;

				while (ica != null)
				{
					CopyFieldsToPropertiesIfNeeded(this, ica, new string[] { "Category", "ExpirationTime", "GlichemicalIndex", "PotencialAlkalinity", "GrammsPerLiter", "GrammsPerPiece", "CaloriesPer100Gramms", "CarbohydratesPer100Gramms", "ProteinPer100Gramms", "FatPer100Gramms", "PieceCountEstimation", "StandardPortionCalories" });

					if (ica.DefaultChild != null)
					{
						ica = (IngredientConstsAttribute)ica.DefaultChild.GetCustomAttributes(typeof(IngredientConstsAttribute), false).FirstOrDefault();
					}
					else
					{
						break;
					}
				}
            }

			if (!this.GrammsPerLiter.HasValue)
			{
				this.GrammsPerLiter = 1000.0f;
			}

			this.PieceCount = this.PieceCountEstimation.HasValue ? (int)(this.PieceCountEstimation * amount) : 1;
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
                    float amount = GetAmount(this.PreferredUnit);

                    if (amount > 0)
                    {
						if (amount == Math.Round(amount))
						{
							amountStr = SignificantDigits.ToString(amount, 1);
						}
						else
						{
							amountStr = SignificantDigits.ToString(amount, 2);
						}

						amountStr += " " + NameAliasAttribute.GetName(languageISO, typeof(MeasurementUnit), this.PreferredUnit.ToString()) + " ";
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
