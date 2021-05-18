using System;
using System.Collections.Generic;
using System.IO;

namespace Lab6.BasicConstructions.Mesh
{
    public class Mesh
    {
        public List<Triangle> Triangles;
        public List<Vector> Normals;
        
        public Mesh(List<Triangle> triangles)
        {
            Triangles = triangles;
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

        public void Parse(string path)
        {
            Triangles = new List<Triangle>();
            var points = new List<Point>();
            if (File.Exists(path))
            {
                using (StreamReader sr = File.OpenText(path))
                {
                    string line = "";
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0] != 'v')
                        {
                            continue;
                        }

                        var splitLine = line.Split(" ");
                        if (splitLine.Length == 4)
                        {
                            points.Add(new Point(
                                float.Parse(splitLine[1]),
                                float.Parse(splitLine[2]),
                                float.Parse(splitLine[3])
                            ));
                        }
                    }
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (line[0] == '#')
                        {
                            continue;
                        }
                        var splitLine = line.Split(" ");
                        if (splitLine.Length == 4)
                            if (splitLine[0] == "vn")
                            {
                                Normals.Add(new Vector(
                                    float.Parse(splitLine[1]),
                                    float.Parse(splitLine[2]),
                                    float.Parse(splitLine[3])
                                    ));
                            }
                    }
                }
            }
        }
    }
}