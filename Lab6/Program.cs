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
            Triangle tr = new Triangle(new Point(10, 0, 0), new Point(10, 1, 0), new Point(10, 0, 1));
            List<Color> colors = cum.GetColors(tr);
            for (int i = 0; i < resx; i++)
            {
                for (int j = 0; j < resy; j++)
                {
                    Console.Write(colors[i * resx + j] == Color.Black ? " " : "#");
                }

                Console.WriteLine();
            }
        }
    }
}