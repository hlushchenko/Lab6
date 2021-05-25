using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6.BasicConstructions.Mesh
{
    public class Triangle
    {
        public Point[] Verticles;
        public Vector Normal;

        public Point MinPoint => new Point(
            Math.Min(Verticles[0].X, Math.Min(Verticles[1].X, Verticles[2].X)),
            Math.Min(Verticles[0].Y, Math.Min(Verticles[1].Y, Verticles[2].Y)),
            Math.Min(Verticles[0].Z, Math.Min(Verticles[1].Z, Verticles[2].Z))
        );
        
        public Point MaxPoint => new Point(
            Math.Max(Verticles[0].X, Math.Max(Verticles[1].X, Verticles[2].X)),
            Math.Max(Verticles[0].Y, Math.Max(Verticles[1].Y, Verticles[2].Y)),
            Math.Max(Verticles[0].Z, Math.Max(Verticles[1].Z, Verticles[2].Z))
        );
        
        public Triangle(params Point[] verticles)
        {
            Verticles = verticles;
        }

        public override string ToString()
        {
            return $"[ {Verticles[0]} ; {Verticles[1]} ; {Verticles[2]} ], n{Normal}";
        }

        public float Weight => avg(Verticles[0].X, Verticles[1].X, Verticles[2].X) +
                                  avg(Verticles[0].Y, Verticles[1].Y, Verticles[2].Y) +
                                  avg(Verticles[0].Z, Verticles[1].Z, Verticles[2].Z);


        private float avg(float a, float b, float c)
        {
            return (a + b + c) / 3;
        }

        public Triangle(Triangle triangle)
        {
            Point[] copy = new Point[3];
            triangle.Verticles.CopyTo(copy, 0);
            Verticles = copy.ToArray();
            Normal = triangle.Normal;
        }
    }
}