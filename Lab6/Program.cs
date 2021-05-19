﻿using System;
using System.Collections.Generic;
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
            Camera cum = new Camera(60, resx, resy, 0, 0, 0, new Vector(1, 0, 0));
            Triangle tr1 = new Triangle(new Point(10, 0, 0), new Point(10, 1, 0), new Point(10, 0, 1));
            Triangle tr2 = new Triangle(new Point(15, 2, 2), new Point(15, 3, 2), new Point(15, 2, 3));
            List<Color> colors = cum.GetColors(new []{tr1, tr2});
            for (int i = 0; i < resx; i++)
            {
                for (int j = 0; j < resy; j++)
                {
                    Console.Write(colors[i * resx + j] == Color.Black ? " " : "#");
                }

                Console.WriteLine();
            }
            // var m = new Mesh(new Ngon[]{});
            // m.Parse(@"C:\Users\Doncr\Desktop\KPI\ОП6\cube.obj");
            // Console.WriteLine("Triangles:");
            // foreach (var t in m.Triangles)
            // {
            //     Console.WriteLine(">"+t);
            // }
        }
    }
}