using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredientContainer
    {
        Dimensions Dimensions { get; }

        IIngredient Contents { get; }

        bool Berakni(IIngredient ig);

        IIngredient Kivenni();

        void Varni(int minutes);

        string GetName(string languageISO);
    }
}

