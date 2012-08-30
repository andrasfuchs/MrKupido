using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MrKupido.Library
{
    public class Dimensions
    {
        public float Width { set; get; }
        public float Height { set; get; }
        public float Depth { set; get; }

        public Dimensions(float width, float height, float depth)
        {
            this.Width = width;
            this.Height = height;
            this.Depth = depth;
        }
    }
}
