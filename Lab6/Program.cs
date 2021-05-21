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
            int resx = 60;
            int resy = 60;
            Camera cum = new Camera(60, resx, resy, -2, -2, -2, new Vector(1, 1, 1));
            Light lite = new Light(new Point(-2, -2, -2), 20, Color.White);
            Mesh meh = new Mesh(resourcesPath + "Sphere.obj");
            Scene mainScene = new Scene(cum, lite, meh);
            cum.Screenshot(resourcesPath + "result.bmp");
            
        }
    }
}