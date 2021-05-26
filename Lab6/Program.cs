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
            int resx = 200;
            int resy = 200;
            Camera cum = new Camera(60, resx, resy, 0, 0, 5, new Vector(0, 0, -1));
            Light lite1 = new Light(new Point(0, 0, 5), 10,new Color("#159957"));
            //Light lite2 = new Light(new Point(2, 0, 3), 6, new Color("#155799"));
            Mesh meh = new Mesh(@"C:\Users\UserPRO\Desktop\labs\Lab6\Lab6\Resources\Sphere.obj", Color.White);
            Scene mainScene = new Scene(cum, lite1, meh);
            //mainScene.AddLight(lite2);
            mainScene.Background = Color.Black;
            // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            //mainScene.EmbientColor = new Color(0.1f, 0.1f, 0.1f);
            cum.Screenshot(@"C:\Users\UserPRO\Desktop\labs\Lab6\Lab6\Resources\result.bmp");
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