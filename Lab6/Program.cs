﻿using System;
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
            //bereza.GetMaxPoint();
            /*string resourcesPath = @"..\..\..\Resources\";
            int resx = 80;
            int resy = 80;
            Camera cum = new Camera(60, resx, resy, 0, -3, 0, new Vector(0, 0.90f, 0.1f));
            Light lite = new Light(new Point(2, 0, -3), 20, Color.White);
            Mesh meh = new Mesh(resourcesPath + "Sphere.obj");
            Scene mainScene = new Scene(cum, lite, meh);
            cum.Screenshot(resourcesPath + "result.bmp");*/
            /*int resx = 256;
            int resy = 256;
            Camera cum = new Camera(60, resx, resy, 3, 1f, 0, new Vector(-1, -0.1f, 0));
            Light cameraLight = new Light(new Point(3, 1, 0),1, Color.White);
            Light lite1 = new Light(new Point(-1, 1, 2), 6, Color.Red);
            Light lite2 = new Light(new Point(-1, 1, -2), 6, Color.Blue);
            Mesh meh = new Mesh("D:/shadowTester.obj", /*new Color("#bfbfbf")#1# Color.White);
            Scene mainScene = new Scene(cum, lite1, meh);
            mainScene.AddLight(lite2);
            mainScene.AddLight(cameraLight);
            mainScene.Background = Color.Black;
            // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            mainScene.EmbientColor = new Color("#0d0d0d");
            cum.Screenshot("D:/Sphere.bmp");*/

            Stopwatch a = new Stopwatch();
            a.Start();
            int resx = 512;
            int resy = 512;
            Camera cum = new Camera(60, resx, resy, 0.75f, 0.2f, 0, new Vector(-1, 0, 0));
            Light lite1 = new Light(new Point(2, 1, -3), 9,new Color("#159957"));
            Light lite2 = new Light(new Point(0.75f, 0.2f, 0), 9, new Color("#155799"));
            
            Mesh meh = new Mesh("D:/cow.obj", Color.White);
            Scene mainScene = new Scene(cum, lite1, meh);
            mainScene.AddLight(lite2);
            mainScene.Background = Color.Black;
            // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            //mainScene.EmbientColor = new Color(0.1f, 0.1f, 0.1f);
            cum.Screenshot("D:/Sphere.bmp");
            a.Stop();
            Console.WriteLine(a.Elapsed);
            Console.ReadKey();
        }

        
    }
}