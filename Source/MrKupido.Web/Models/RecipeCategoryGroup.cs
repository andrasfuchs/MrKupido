using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Library;
using MrKupido.Processor.Model;

namespace MrKupido.Web.Models
{
    public class RecipeCategoryGroup
    {
        public ShoppingListCategory Category = ShoppingListCategory.Unknown;
        public List<RecipeSearchResultItem> Items = new List<RecipeSearchResultItem>();
    }
}