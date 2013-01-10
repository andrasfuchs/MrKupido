using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Attributes
{

    /// <summary>
    /// This class is used to define constant for an ingredient or recipe class. Attributes are used because we need this information without instanciation. 
    /// After the class is created all these values are copied to class properties and they should be used in logic.
    /// </summary>
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

        /// <summary>
        /// True if it is more like a category not a physical ingredient. Abstract ingredients must have the DefaultChild defined 
        /// and they should be replaced by any of its descendants in the recipe.
        /// If this set and applied for a recipe, the recipe should not have an implementation.
        /// </summary>
        public bool IsAbstract; 
        /// <summary>
        /// The default substitue of the ingredient. For example the default for fat is lard, for oil is sunflower oil.
        /// This must be set if the IsAbstact is set to true.
        /// </summary>
        public Type DefaultChild;

        /// <summary>
        /// True if the recipe should be handled as an inline ingredient, so the making of it must be part of the final recipe. 
        /// </summary>
        public bool IsInline;

        /// <summary>
        /// Ingrecs are recipes which should be handled as ingredients. 
        /// </summary>
        public bool IsIngrec;
    }
}
