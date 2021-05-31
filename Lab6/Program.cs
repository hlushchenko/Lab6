using System;
using System.Diagnostics;
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
            Stopwatch a = new Stopwatch();
            a.Start();
            int resx = 1024;
            int resy = 1024;
            Camera camera = new Camera(60, resx, resy, 1f, 0f, 0, new Vector(-1, 0, 0));
            Light light = new Light(new Point(2, 0, -3), 6,new Color("#159957"));
            //Light light2 = new Light(new Point(2, 0, 3), 6, new Color("#155799"));
            Mesh meh = new Mesh("D:/cow.obj", Color.White);
            Scene mainScene = new Scene(camera, light, meh);
            //mainScene.AddLight(light2);
            mainScene.Background = Color.Black;
            // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            //mainScene.EmbientColor = new Color(0.1f, 0.1f, 0.1f);
            camera.Screenshot("D:/Sphere.bmp");
            a.Stop();
            Console.WriteLine(a.Elapsed);
            Console.ReadKey();
        }

        
    }
}