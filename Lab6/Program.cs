using System;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions;


namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            int resx = 80;
            int resy = 80;
            Camera cum = new Camera(60, resx, resy, 0, 0, 0, new Vector( 1, 0, 0));
            cum.GetRays();
            Triangle tr = new Triangle(new Point(10, 0, 0), new Point(10,1,0), new Point(10,0,1));
            
        }
    }
}