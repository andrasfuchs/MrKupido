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
        public IngredientState State { get; set; }

        public int PieceCount { get; set; }

        public ShoppingListCategory? Category { get; protected set; }

        public int? ExpirationTime { get; protected set; }
        public float? StorageTemperature { get; protected set; }
        public int? GlichemicalIndex { get; protected set; }
        public float? PotencialAlkalinity { get; protected set; }

        public SingleIngredient(float amount, MeasurementUnit unit, IngredientState state = IngredientState.Normal) : base(amount, unit)
        {
            this.State = state;
            this.PieceCount = 1;

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
