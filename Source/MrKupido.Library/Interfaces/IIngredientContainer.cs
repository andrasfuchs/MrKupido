using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public interface IIngredientContainer
    {
        Dimensions Dimensions { get; }

        int Id { get; set; }

        IIngredient Contents { get; }

        void Berakni(params IIngredient[] ingredients);

        IIngredient Kivenni();

        void Varni(int minutes);

        string GetName(string languageISO);

        void Add(IIngredient i);
        void AddRange(IIngredient[] i);
        void Empty();
    }
}

