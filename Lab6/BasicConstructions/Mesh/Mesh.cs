using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Lab6.BasicConstructions.RTree;

namespace Lab6.BasicConstructions.Mesh
{
    public class Mesh
    {
        public List<Triangle> Triangles;
        public Color Color;
        public Node Head;

        public Mesh(List<Triangle> triangles)
        {
            Triangles = triangles;
        }

        public List<Triangle> GetTriangles(Ray ray)
        {
            var result = new List<Triangle>();
            ray.NewIntersect(Head,result);
            return result;
        }

        public Mesh(Ngon[] ngons)
        {
            foreach (var ngon in ngons)
            {
                var tempTriangles = ngon.Triangulate();
                foreach (var triangle in tempTriangles)
                {
                    Triangles.Add(triangle);
                }
            }
        }

        public Mesh(string path, Color color)
        {
            Color = color;
            var culture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
            culture.NumberFormat.NumberDecimalSeparator = ".";

            Triangles = new List<Triangle>();

            var points = new List<Point>();
            var normals = new List<Vector>();

            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0] == '#')
                        {
                            continue;
                        }

                        var splitLine = line.Split(" ");
                        if (splitLine.Length == 4 && splitLine[0] == "v")
                        {
                            points.Add(new Point(
                                float.Parse(splitLine[1], culture),
                                float.Parse(splitLine[2], culture),
                                float.Parse(splitLine[3], culture)
                            ));
                        }

                        if (splitLine.Length == 4)
                            if (splitLine[0] == "vn")
                            {
                                normals.Add(new Vector(
                                    float.Parse(splitLine[1], culture),
                                    float.Parse(splitLine[2], culture),
                                    float.Parse(splitLine[3], culture)
                                ));
                            }
                    }
                }

                using (StreamReader sr = File.OpenText(path))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0] == '#')
                        {
                            continue;
                        }

                        var splitLine = line.Split(" ");
                        if (splitLine[0] == "f")
                        {
                            var tempNgon = new Ngon();
                            tempNgon.Verticles = new List<Point>();
                            
                            for (int i = 1; i < splitLine.Length; i++)
                            {
                                var curSplit = splitLine[i].Split("/");
                                if (i == 1)
                                {
                                    tempNgon.Normal = normals[int.Parse(curSplit[2]) - 1];
                                }

                                tempNgon.Verticles.Add(points[int.Parse(curSplit[0]) - 1]);
                            }

                            var triangulated = tempNgon.Triangulate();
                            if (Head == null)
                            {
                                Head = new Node(triangulated[0]);
                                triangulated.RemoveAt(0);
                            }

                            foreach (var triangle in triangulated)
                            {
                                Head.Insert(triangle);
                            }
                            //Triangles.AddRange(tempNgon.Triangulate());
                        }
                    }
                }
                Console.WriteLine(Head);
            }
        }
    }
}