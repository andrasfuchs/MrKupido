using System;

namespace MrKupido.Library.Attributes
{

    /// <summary>
    /// This class is used to define constant for an ingredient or recipe class. Attributes are used because we need this information without instanciation. 
    /// After the class is created all these values are copied to class properties and they should be used in logic.
    /// </summary>
    [AttributeUsage(System.AttributeTargets.Class, AllowMultiple = false)]
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
        public int ExpirationTime = Int32.MinValue;

        /// <summary>
        /// Storage temperature in degree Celsius
        /// source: http://www.engineeringtoolbox.com/fruits-vegetables-storage-conditions-d_710.html
        /// </summary>
        public float StorageTemperature = Single.MinValue;

        /// <summary>
        /// Glichemical index between 1 and 100
        /// </summary>
        public int GlichemicalIndex = Int32.MinValue;

        public int InflammationFactor = Int32.MinValue;

        /// <summary>
        /// Relative potencial of alkalinity (+) or acidity (-) between +50 and -50
        /// source: http://www.balance-ph-diet.com/acid_alkaline_food_chart.html
        /// </summary>
        public float PotencialAlkalinity = Single.MinValue;

        public float GrammsPerLiter = Single.MinValue;
        public float GrammsPerPiece = Single.MinValue;
        public float CaloriesPer100Gramms = Single.MinValue;
        public float CarbohydratesPer100Gramms = Single.MinValue;
        public float FatPer100Gramms = Single.MinValue;
        public float ProteinPer100Gramms = Single.MinValue;

        /// <summary>
        /// Indicates the calories value which should be handled as one portion. The portion multiplier is altered in runtime to standardize it to this number of calories when giving the 1.0 multiplier as parameter to the recipe.
        /// </summary>
        public float StandardPortionCalories = 1000.0f;

        /// <summary>
        /// If the recipe will consist of more than one unit, this property should indicate the estimated number of units cooked by the recipe with the 1.0 multiplier
        /// </summary>
        public float PieceCountEstimation = Single.MinValue;

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

        /// <summary>
        /// ManTags are defined here, separated by comma. ManTags stands for manual tags and they are used on recipes only.
        /// </summary>
        public string ManTags;
    }
}
