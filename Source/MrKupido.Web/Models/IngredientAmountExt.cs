using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MrKupido.Model;
using MrKupido.DataAccess;

namespace MrKupido.Web.Models
{
    public static class IngredientAmountExt
    {
        public static MrKupidoContext Context;

        public static string[] Warnings(this IngredientAmount ia)
        {
            List<string> result = new List<string>();

            if (ia.Amount == 0) result.Add("WARNING: amount should not be zero");

            if (String.IsNullOrEmpty(ia.IngredientUniqueName))
            {
                result.Add("ERROR: uniquename should not be empty");
            }
            else
            {
                if (ia.IngredientUniqueName.StartsWith("ERROR")) result.Add(ia.IngredientUniqueName);
            }

            return result.ToArray();
        }
    }
}