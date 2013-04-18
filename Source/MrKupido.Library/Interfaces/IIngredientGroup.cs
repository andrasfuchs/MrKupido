using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredientGroup : IIngredient
    {
        int Id { set; get; }

        ISingleIngredient[] Ingredients { get; }

        int IngredientCount { get; }

        void AddIngredients(params IIngredient[] ingredients);

		ISingleIngredient RemoveIngredient(ISingleIngredient i);
    }
}
