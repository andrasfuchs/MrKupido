using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Library;
using MrKupido.Processor.Model;
using MrKupido.Processor;
using System.Text;
using MrKupido.Library.Attributes;
using MrKupido.Utils;
using System.Threading;
using System.Diagnostics;
using MrKupido.Library.Ingredient;

namespace MrKupido.Web.Models
{
    public class RecipeSearchResultItem
    {
		public int Index;

        public ShoppingListCategory MainCategory;
        public string IconUrl;
        public string DisplayName;
        public string UniqueName;
        public string ParentUniqueName;
		public string Version;

        public bool IsSelected;
        public int NetTime;
        public int TotalTime;
        public bool IsVegetarian;
        public bool IsGlutenFree;
        public bool IsLactoseFree;
        public string[] Photos;
        public int SubVersions;
        
        public string MainIngredients;
        public string CommercialInfo;
        public string CommercialIconFilename;
        
        public float? Rating;
        public int RatingCount;

		public float TotalWeight;
		public float? TotalCalories = null;
		public float TotalCaloriesCompletion;
		public float? TotalCarbohydrates = null;
		public float TotalCarbohydratesCompletion;
		public float? TotalProtein = null;
		public float TotalProteinCompletion;
		public float? TotalFat = null;
		public float TotalFatCompletion;

		public float? CaloriesLevel;
		public float? CarbohydratesLevel;
		public float? ProteinLevel;
		public float? FatLevel;

        public bool IsImplemented;
        public bool IsHidden;
        public string CSSClass;

        public RecipeSearchResultItem(RecipeTreeNode rtn)
        {
            if (String.IsNullOrEmpty(rtn.ShortName)) throw new MrKupidoException("Recipe '{0}' should have a ShortName defined.", rtn.UniqueName);

            this.DisplayName = Char.ToUpper(rtn.ShortName[0]) + rtn.ShortName.Substring(1);
            if (this.DisplayName.Length > 40)
            {
                Trace.TraceWarning("The display name of the recipe should be not longer than 40 characters. '{0}' is longer than that.", this.DisplayName);
            }

            this.UniqueName = rtn.UniqueName;
            this.ParentUniqueName = rtn.Parent == null ? null : rtn.Parent.UniqueName;
			this.Version = rtn.Version;

            this.SubVersions = TreeNode.GetDescendantCount(rtn);
            this.IconUrl = rtn.IconUrl;

            StringBuilder sb = new StringBuilder();
			
			RuntimeIngredient[] ingredients = rtn.GetIngredients(1.0f, 1);
			foreach (RuntimeIngredient i in ingredients)
			{
				if (!String.IsNullOrEmpty(i.RecipeUniqueName) && String.IsNullOrEmpty(i.RecipeName))
				{
					i.RecipeName = Cache.Recipe[i.RecipeUniqueName].ShortName;
				}

				sb.Append(i.Ingredient.GetName(rtn.LanguageISO));
				sb.Append(", ");
			}

            if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);
            
			this.MainIngredients = sb.ToString();
			this.TotalWeight = rtn.TotalWeight;
			this.TotalCalories = rtn.TotalCalories;
			this.TotalCaloriesCompletion = rtn.TotalCaloriesCompletion;
			this.TotalCarbohydrates = rtn.TotalCarbohydrates;
			this.TotalCarbohydratesCompletion = rtn.TotalCarbohydratesCompletion;
			this.TotalProtein = rtn.TotalProtein;
			this.TotalProteinCompletion = rtn.TotalProteinCompletion;
			this.TotalFat = rtn.TotalFat;
			this.TotalFatCompletion = rtn.TotalFatCompletion;

            if (this.IconUrl == null)
            {
                this.IconUrl = PathUtils.GetActualUrl(IconUriFragmentAttribute.GetUrls(this.MainCategory.GetType(), "~/Content/svg/cat_{0}.svg", this.MainCategory.ToString()));
            }

            IDirection[] directions = rtn.GetDirections(1.0f, 1);

            this.TotalTime = (int)directions.Select(d => d.TimeToComplete).Select(t => t.TotalMinutes).Sum();
            this.NetTime = (int)directions.Where(d => !d.IsPassive).Select(d => d.TimeToComplete).Select(t => t.TotalMinutes).Sum();

			if (this.TotalCaloriesCompletion > 0.75)
			{
				CaloriesLevel = this.TotalCalories.Value / this.TotalWeight;

				if (this.TotalCarbohydratesCompletion > 0.75)
				{
					CarbohydratesLevel = this.TotalCarbohydrates.Value / this.TotalCalories.Value;
				}

				if (this.TotalProteinCompletion > 0.75)
				{
					ProteinLevel = this.TotalProtein.Value / this.TotalCalories.Value;
				}

				if (this.TotalFatCompletion > 0.75 )
				{
					FatLevel = this.TotalFat.Value / this.TotalCalories.Value;
				}
			}

            this.IsHidden = rtn.IsIngrec || rtn.IsInline || (rtn.CommercialAttribute != null);
            this.IsImplemented = rtn.IsImplemented;

            if (!rtn.IsImplemented) CSSClass = "notimplemented";

            if (rtn.IsAbtract) CSSClass = "abstract";

            if (rtn.CommercialAttribute != null)
            {
                ProviderBase pb = null;
                sb.Clear();

                if (rtn.CommercialAttribute.Brand != null)
                {
                    pb = (ProviderBase)Activator.CreateInstance(rtn.CommercialAttribute.Brand, rtn.LanguageISO);

                    sb.Append("Márka: ");
                    sb.Append(pb.BrandName);
                    sb.Append(", ");

                    CommercialIconFilename = pb.IconFilename;
                }

                if (rtn.CommercialAttribute.MadeBy != null)
                {
                    pb = (ProviderBase)Activator.CreateInstance(rtn.CommercialAttribute.MadeBy, rtn.LanguageISO);

                    sb.Append("Gyártja: ");
                    sb.Append(pb.CompanyName);
                    sb.Append(", ");
                    sb.Append(pb.Town);
                    sb.Append(", ");
                }

                if (rtn.CommercialAttribute.DistributedBy != null)
                {
                    pb = (ProviderBase)Activator.CreateInstance(rtn.CommercialAttribute.DistributedBy, rtn.LanguageISO);

                    sb.Append("Terjeszti: ");
                    sb.Append(pb.CompanyName);
                    sb.Append(", ");
                    sb.Append(pb.Town);
                    sb.Append(", ");
                }

                if (rtn.CommercialAttribute.BarCode != null)
                {
                    sb.Append("Vonalkód: ");
                    sb.Append(rtn.CommercialAttribute.BarCode);
                    sb.Append(", ");
                }

                if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);

                CommercialInfo = sb.ToString();

                CSSClass = "commercial";
            }


			// get the images
			List<string> photos = new List<string>();
			for (int i = 1; i <= 99; i++)
			{
				string url = String.Format("~/Content/photos/960x480/{0}_{1}.jpg", rtn.UniqueNameEng, i.ToString("00"));

				url = PathUtils.GetActualUrl(new string[] { url });

				if (url != null)
				{
					photos.Add(url);
				}
			}
			this.Photos = photos.ToArray();


			// check true/false properties
			this.IsVegetarian = rtn.GetTags().Any(tag => tag.ClassName == "Vegetarian");
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}