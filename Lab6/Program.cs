using System;
using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions.Objects;
using Lab6.BasicConstructions.RTree;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            string resourcesPath = @"..\..\..\Resources\";
            int resx = 80;
            int resy = 80;
            Camera cum = new Camera(60, resx, resy, 0, -3, 0, new Vector(0, 0.90f, 0.1f));
            Light lite = new Light(new Point(2, 0, -3), 20, Color.White);
            Mesh meh = new Mesh(resourcesPath + "Sphere.obj");
            Scene mainScene = new Scene(cum, lite, meh);
            cum.Screenshot(resourcesPath + "result.bmp");
            
            mainScene.Background = Color.Black;
        }

        private const int size = 5;
        private const float tsize = 0.4f;
        

        public static Triangle candom()
        {
            var Rand = new Random();
            var A = new Point((float) Rand.NextDouble() * size * 2 - size,
                (float) Rand.NextDouble() * size * 2 - size,
                (float) Rand.NextDouble() * size * 2 - size);
            var B = new Point(A);
            B.Translate((float) (Rand.NextDouble() * tsize * 2 - tsize),
                (float) (Rand.NextDouble() * tsize * 2 - tsize), (float) (Rand.NextDouble() * tsize * 2 - tsize));
            var C = new Point(B);
            C.Translate((float) (Rand.NextDouble() * tsize * 2 - tsize),
                (float) (Rand.NextDouble() * tsize * 2 - tsize), (float) (Rand.NextDouble() * tsize * 2 - tsize));
            return new Triangle(A,B,C);
        }
    }
}