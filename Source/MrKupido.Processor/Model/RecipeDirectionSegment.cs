using MrKupido.Library;

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
