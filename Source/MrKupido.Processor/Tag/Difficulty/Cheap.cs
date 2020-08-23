using MrKupido.Library.Attributes;
using MrKupido.Processor.Model;
using System.Linq;

namespace MrKupido.Library.Tag
{
    [NameAlias("eng", "easy")]
    [NameAlias("hun", "könnyű")]
    public class Easy : TagBase
    {
        public override float Match(ITreeNode r)
        {
            if (r is RecipeTreeNode)
            {
                RecipeTreeNode rtn = (RecipeTreeNode)r;

                if (rtn.ManTags.Split(',').Contains(this.GetType().Name)) return 1.0f;
            }

            return 0.0f;
        }
    }
}
