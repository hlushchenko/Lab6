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
            int resx = 64;
            int resy = 64;
            Camera cum = new Camera(60, resx, resy, 0, -2, 0, new Vector(0, 1, 0));
            Light lite = new Light(new Point(0, -2, 0), 200000, Color.White);
            Mesh meh = new Mesh("D:/Cow.obj");
            Scene mainScene = new Scene(cum, lite, meh);
            cum.Screenshot("D:/sphere.bmp");
            
        }
    }
}