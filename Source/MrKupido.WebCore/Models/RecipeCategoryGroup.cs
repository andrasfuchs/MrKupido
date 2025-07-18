using MrKupido.Library;
using System.Collections.Generic;

namespace MrKupido.Web.Models
{
    public class RecipeCategoryGroup
    {
        public ShoppingListCategory Category = ShoppingListCategory.Unknown;
        public List<RecipeSearchResultItem> Items = new List<RecipeSearchResultItem>();
    }
}