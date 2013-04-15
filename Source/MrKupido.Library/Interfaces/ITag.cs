using MrKupido.Library.Ingredient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface ITag
    {
		float Match(IIngredient i);

		bool IsMatch(IIngredient i);

		float Match(IRecipe r);

		bool IsMatch(IRecipe r);
    }
}
