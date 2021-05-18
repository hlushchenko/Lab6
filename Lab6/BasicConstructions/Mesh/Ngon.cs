using System;
using System.Collections.Generic;

namespace Lab6.BasicConstructions.Mesh
{
    public class Ngon
    {
        public Point[] Verticles;

        public Ngon(params Point[] verticles)
        {
            Verticles = verticles;
        }

        public List<Triangle> Triangulate()
        {
            var result = new List<Triangle>();

            var pointList = new List<Point>();
            pointList.AddRange(Verticles);

            //Заготовка під нормальні полігони
            while (pointList.Count != 3)
            {
                TriangulateUtil(pointList, result);
            }

            return result;
        }

        private void TriangulateUtil(List<Point> verticles, List<Triangle> result)
        {
            var copy = verticles;
            var current = verticles[0];
            
            Triangle temp;
            
            for (var i = 2; i < copy.Count - 1; i++)
            {
                temp = new Triangle(new Point[] {verticles[0], verticles[i - 1], verticles[i]});
                result.Add(temp);
                verticles.RemoveAt(i - 1);
                i--;
            }

            temp = new Triangle(new Point[] {verticles[0], verticles[1], verticles[2]});
            result.Add(temp);
        }
    }
}