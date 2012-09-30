using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MrKupido.Library;

namespace MrKupido.Model
{
    public class Ingredient : FilterItem
    {
        public override string ToString()
        {
            return this.NameHun;
        }
    }
}
