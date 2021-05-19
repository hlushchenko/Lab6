namespace Lab6.BasicConstructions
{
    public class Color
    {
        public float R;
        public float G;
        public float B;

        public Color(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static readonly Color White = new(1, 1, 1);
        public static readonly Color Black = new(0, 0, 0);

        public static Color operator +(Color color1, Color color2)
        {
            return new Color(color1.R + color2.R, color1.G + color2.G, color1.B + color2.B);
        }
        
        public static Color operator *(Color color1, Color color2)
        {
            return new Color(color1.R * color2.R, color1.G * color2.G, color1.B * color2.B);
        }
        public static Color operator *(Color color1, float mult)
        {
            if (mult > 0)
            {
                return new Color(color1.R * mult, color1.G * mult, color1.B * mult);
            }
            return Black;
        }
    }
}