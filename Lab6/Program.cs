using System;
using System.Collections.Generic;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Objects;


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
    }
}