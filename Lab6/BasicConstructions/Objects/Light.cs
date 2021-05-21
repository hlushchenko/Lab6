using System;
using Lab6.BasicConstructions.Mesh;

namespace Lab6.BasicConstructions.Objects
{
    public class Light : Object
    {
        public int Power;
        public Color LightColor;

        public Light(Point position, int power, Color lightColor)
        {
            Power = power;
            LightColor = lightColor;
            Position = position;
        }

        public Color Shade(Point point, Triangle triangle)
        {
            Vector direction = new Vector(point, Position);
            Color output = LightColor * direction.AngleCos(triangle.Normal) * (1/MathF.Pow(direction.Length(), 2));
            return output;
        }
    }
}