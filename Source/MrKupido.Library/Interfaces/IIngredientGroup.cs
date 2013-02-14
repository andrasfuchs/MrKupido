using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredientGroup : IIngredient
    {
        int Id { set; get; }

        string IconUrl { get; set; }

        ISingleIngredient[] Ingredients { get; }

        int Count { get; }

        void AddIngredients(params IIngredient[] ingredients);
    }
}
