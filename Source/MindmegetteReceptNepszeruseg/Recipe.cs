using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MindmegetteReceptNepszeruseg
{
    public class Recipe
    {
        public string UniqueName;
        public int Favourited;

        public override string ToString()
        {
            return String.Format("{0},{1}", UniqueName, Favourited);
        }
    }
}
