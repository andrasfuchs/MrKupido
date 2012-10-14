using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Library;
using MrKupido.Processor.Model;
using MrKupido.Processor;
using System.Text;

namespace MrKupido.Web.Models
{
    public class RecipeSearchResultItem
    {
        public ShoppingListCategory MainCategory;
        public string DisplayName;
        public string UniqueName;
        public bool IsSelected;
        public int TotalTime;
        public bool IsVegetarian;
        public bool IsGlutenFree;
        public bool IsLactoseFree;
        public Uri[] Photos;
        public int SubVersions;
        
        public string MainIngredients;
        
        public float? Rating;
        public int RatingCount;

        public float? Cals;
        public float? Sugar;
        public float? Fat;
        public float? Salt;

        public RecipeSearchResultItem(RecipeTreeNode rtn)
        {
            this.DisplayName = Char.ToUpper(rtn.ShortName[0]) + rtn.ShortName.Substring(1);
            this.UniqueName = rtn.UniqueName;

            StringBuilder sb = new StringBuilder();
            foreach (IIngredient i in rtn.GetIngredients(1.0f))
            {
                sb.Append(i.Name);
                sb.Append(", ");
            }
            if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);

            this.MainIngredients = sb.ToString();
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}