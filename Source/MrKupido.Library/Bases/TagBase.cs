using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MrKupido.Library.Attributes;

namespace MrKupido.Library.Tag
{
    [NameAlias("eng", "tag")]
    [NameAlias("hun", "címke")]
    public class TagBase : NamedObject, ITag
    {
		public virtual float Match(IIngredient i)
		{
			return (i is IRecipe) ? Match((IRecipe)i) : 0.0f;
		}

		public virtual bool IsMatch(IIngredient i)
		{
			return (i is IRecipe) ? IsMatch((IRecipe)i) : Match(i) == 1.0f;
		}

		public virtual float Match(IRecipe r)
		{
			return 0.0f;
		}

		public virtual bool IsMatch(IRecipe r)
		{
			return Match(r) == 1.0f;
		}	
	}
}
