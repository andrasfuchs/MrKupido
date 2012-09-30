using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{
    public class IngredientConstsAttribute : Attribute
    {
        /// <summary>
        /// A category where you can find this item in a supermarket
        /// </summary>
        public ShoppingListCategory Category;

        /// <summary>
        /// Expiration in days
        /// source: http://www.engineeringtoolbox.com/fruits-vegetables-storage-conditions-d_710.html
        /// </summary>
        public int ExpirationTime;

        /// <summary>
        /// Storage temperature in degree Celsius
        /// source: http://www.engineeringtoolbox.com/fruits-vegetables-storage-conditions-d_710.html
        /// </summary>
        public float StorageTemperature;

        /// <summary>
        /// Glichemical index between 1 and 100
        /// </summary>
        public int GlichemicalIndex;
        
        /// <summary>
        /// Relative potencial of alkalinity (+) or acidity (-) between +50 and -50
        /// source: http://www.balance-ph-diet.com/acid_alkaline_food_chart.html
        /// </summary>
        public float PotencialAlkalinity;

        public float GrammsPerLiter;
        public float GrammsPerPiece;
        public float KCaloriesPerGramm;
    }
}
