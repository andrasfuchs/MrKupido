using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Library;
using MrKupido.Processor.Model;
using MrKupido.Processor;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Web.Models
{
    public class RecipeSearchResultItem
    {
        public ShoppingListCategory MainCategory;
        public string IconUrl;
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
            this.SubVersions = TreeNode.GetDescendantCount(rtn);
            this.IconUrl = rtn.IconUrl == null ? null : VirtualPathUtility.ToAbsolute(rtn.IconUrl);

            StringBuilder sb = new StringBuilder();
            foreach (IIngredient i in rtn.GetIngredients(1.0f))
            {
                if ((this.MainCategory == ShoppingListCategory.Unknown) && (i.Category.HasValue))
                {
                    this.MainCategory = i.Category.Value;
                }

                sb.Append(i.Name);
                sb.Append(", ");
            }
            if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);
            this.MainIngredients = sb.ToString();

            if (this.IconUrl == null)
            {
                this.IconUrl = VirtualPathUtility.ToAbsolute(IconUriFragmentAttribute.GetUrl(this.MainCategory.GetType(), "~/Content/svg/cat_{0}.svg", this.MainCategory.ToString()));
            }

            //if ((this.IconUrl == null) || (!System.IO.File.Exists(this.IconUrl)))
            //{
            //    this.IconUrl = VirtualPathUtility.ToAbsolute(IconUriFragmentAttribute.GetUrl(typeof(ShoppingListCategory), "~/Content/svg/cat_{0}.svg", ShoppingListCategory.Unknown.ToString()));
            //}

            this.TotalTime = (int)rtn.GetDirections(1.0f).Select(d => d.TimeToComplete).Select(t => t.TotalMinutes).Sum();
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}