using MrKupido.Library;
using MrKupido.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Processor.Model
{
    public class RecipeDirectionSegment : IDirectionSegment
    {
        public string Text { get; set; }

        public RecipeDirectionSegment(string text)
        {
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text;
        }

        public virtual string TextOnly()
        {
            return this.Text;
        }
    }
}
