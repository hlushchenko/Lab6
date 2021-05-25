using System;
using System.Runtime.InteropServices.ComTypes;
using Lab6.BasicConstructions.Mesh;

namespace Lab6.BasicConstructions.Objects
{
    public class Light : Object
    {
        public int Power;
        public Color LightColor;
        public Scene Scene;

        public Light(Point position, int power, Color lightColor)
        {
            Power = power;
            LightColor = lightColor;
            Position = position;
        }

        public Color Shade(Point point, Triangle triangle)
        {
            Vector direction = new Vector(point, Position);
            Ray toLight = new Ray(direction, point);
            float distance = 0;
            Point intersect = new Point(0, 0, 0);
            foreach (var tr in Scene.MainObject.Triangles)
            {
                if (toLight.Intersects(tr, ref distance, ref intersect))
                {
                    if (distance < direction.Length() && intersect != point)
                    {
                        return Color.Black;
                    }
                }
            }
            Color output = LightColor * direction.AngleCos(triangle.Normal) * (Power/MathF.Pow(direction.Length(), 2));
            return output;
        }
        
        public static Color MultipleShade(Point intersect, Triangle triangle, Scene scene)
        {
            Color output = Color.Black;
            output += scene.EmbientColor;
            foreach (var light in scene.Light)
            {
                output += light.Shade(intersect, triangle);
            }
            return output;
        }
    }
}