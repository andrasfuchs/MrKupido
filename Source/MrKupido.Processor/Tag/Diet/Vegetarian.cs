using MrKupido.Library.Attributes;
using MrKupido.Library.Ingredient;
using MrKupido.Processor.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library.Tag
{
	[NameAlias("eng", "vegetarian")]
	[NameAlias("hun", "vegetáriánus")]
	public class Vegetarian : TagBase
	{
		public override float Match(ITreeNode r)
		{
			if (r is RecipeTreeNode)
			{
				RecipeTreeNode rtn = (RecipeTreeNode)r;

				if (rtn.GetIngredients(1.0f, 1).Any(rti => (rti.Ingredient is SingleIngredient) && ((SingleIngredient)rti.Ingredient).Category == ShoppingListCategory.Meat))
				{
					return 0.0f;
				}
			}

			return 1.0f;
		}
	}
}
