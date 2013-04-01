﻿using System;
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
        private static Random rnd = new Random();

		public int Index;

        public ShoppingListCategory MainCategory;
        public string IconUrl;
        public string DisplayName;
        public string UniqueName;
        public string ParentUniqueName;

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

        public float? Cals;
        public float? Sugar;
        public float? Fat;
        public float? Salt;

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

            this.SubVersions = TreeNode.GetDescendantCount(rtn);
            this.IconUrl = rtn.IconUrl;

            StringBuilder sb = new StringBuilder();
            foreach (RuntimeIngredient i in rtn.GetIngredients(1.0f, 1))
            {
                if ((this.MainCategory == ShoppingListCategory.Unknown) && (i.Ingredient is SingleIngredient) && (((SingleIngredient)i.Ingredient).Category.HasValue))
                {
                    this.MainCategory = ((SingleIngredient)i.Ingredient).Category.Value;
                }

                sb.Append(i.Ingredient.GetName(rtn.LanguageISO));
                sb.Append(", ");
            }
            if (sb.Length >= 2) sb.Remove(sb.Length - 2, 2);
            this.MainIngredients = sb.ToString();

            if (this.IconUrl == null)
            {
                this.IconUrl = PathUtils.GetActualUrl(IconUriFragmentAttribute.GetUrls(this.MainCategory.GetType(), "~/Content/svg/cat_{0}.svg", this.MainCategory.ToString()));
            }

            IDirection[] directions = rtn.GetDirections(1.0f, 1);

            this.TotalTime = (int)directions.Select(d => d.TimeToComplete).Select(t => t.TotalMinutes).Sum();
            this.NetTime = (int)directions.Where(d => !d.IsPassive).Select(d => d.TimeToComplete).Select(t => t.TotalMinutes).Sum();

            this.Sugar = rnd.Next(5);
            this.Fat = rnd.Next(5);
            this.Salt = rnd.Next(5);

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
        }

        public override string ToString()
        {
            return this.DisplayName;
        }
    }
}