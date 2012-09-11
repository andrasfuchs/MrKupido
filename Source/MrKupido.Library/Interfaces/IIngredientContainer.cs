using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredientContainer
    {
        string Name { get; }

        Dimensions Dimensions { get; set; }

        IIngredient Contents { get; set; }

        bool Berakni(IIngredient ig);

        IIngredient Kivenni();

        void Varni(int minutes);
    }
}

