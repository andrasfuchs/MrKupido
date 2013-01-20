using MrKupido.Library.Equipment;
using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class RecipeDirectionSegmentReference : RecipeDirectionSegment
    {
        public int Id { get; private set; }
        public string IconUrl { get; set; }
        public object Reference { get; private set; }

        public RecipeDirectionSegmentReference(object reference) : base("")
        {
            this.Reference = reference;

            if (reference == null)
            {
                this.Text = "(null)";
            }
            else
            {
                this.Text = reference.ToString();
                if (reference is Container)
                {
                    this.Id = ((Container)reference).Id;
                    this.IconUrl = ((Container)reference).IconUrl;
                }
                else if (reference is IngredientGroup)
                {
                    IngredientGroup ig = (IngredientGroup)reference;

                    this.Id = ig.Id;
                    this.IconUrl = ig.IconUrl;

                    this.Text = String.IsNullOrEmpty(ig.IconUrl) ? this.Text = ig.Name : "--";
                }
            }

            // remove commercial text { }
            int beforeStart = this.Text.IndexOf('{');
            int afterEnd = this.Text.LastIndexOf('}') + 1;

            if ((beforeStart >= 0) && (afterEnd >= 0))
            {
                this.Text = this.Text.Remove(beforeStart, afterEnd - beforeStart).TrimEnd();
            }
        }
    }
}
