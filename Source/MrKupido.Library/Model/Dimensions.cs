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

        public static Dimensions operator *(Dimensions d, float m)
        {
            return new Dimensions(d.Width * m, d.Height * m, d.Depth * m);
        }
    }
}
