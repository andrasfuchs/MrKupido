using MrKupido.Library.Ingredient;

namespace MrKupido.Processor.Model
{
    public class RuntimeIngredient
    {
        public string RecipeUniqueName { get; private set; }
        public string RecipeName { get; set; }

        public IngredientBase Ingredient { get; private set; }
        public TreeNode TreeNode { get; private set; }

        public RuntimeIngredient(IngredientBase i, TreeNode tn, string run)
        {
            Ingredient = i;
            TreeNode = tn;
            RecipeUniqueName = run;
        }
    }
}
