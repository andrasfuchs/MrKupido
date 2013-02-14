using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface ISingleIngredient : IIngredient
    {
        IngredientState State { get; set; }

        int PieceCount { get; set; }
    }
}
