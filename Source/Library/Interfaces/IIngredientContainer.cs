using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredientContainer
    {
        Dimensions Dimensions { get; set; }

        IIngredient Contents { get; set; }

        void Berakni(IIngredient ig);

        IIngredient Kivenni();

        void Varni(int minutes);
    }
}

