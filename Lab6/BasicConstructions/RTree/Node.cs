using System;
using System.Collections.Generic;
using Lab6.BasicConstructions.Mesh;

namespace Lab6.BasicConstructions.RTree
{
    public class Node
    {
        public Point _minPoint;

        public Point _maxPoint;

        //maxChildren>=2
        public const int maxChildren = 20;

        public List<Triangle> Triangles;
        public List<Node> SubNodes;

        public void Insert(Triangle t)
        {
            IncreaseVol(t);

            if (SubNodes.Count != 0)
            {
                if (SubNodes[0].addedVol(t) < SubNodes[1].addedVol(t))
                {
                    SubNodes[0].Insert(t);
                }
                else
                {
                    SubNodes[1].Insert(t);
                }

                return;
            }

            if (Triangles.Count == maxChildren)
            {
                Split();
                Insert(t);
                return;
            }

            Triangles.Add(t);
        }

        public void Split()
        {
            if (Triangles.Count == 0)
            {
                Console.WriteLine("Nothing to split");
            }

            var minTriangle = GetMinPoint();
            SubNodes.Add(new Node(minTriangle.MinPoint,
                minTriangle.MaxPoint, minTriangle));

            var maxTriangle = GetMaxPoint();
            SubNodes.Add(new Node(maxTriangle.MinPoint,
                maxTriangle.MaxPoint, maxTriangle));

            while (Triangles.Count != 0)
            {
                var selected = 0;
                var minId = 0;
                var minArea = -1.0;
                for (int i = 0; i < Triangles.Count; i++)
                {
                    if (i == 0 || minArea > SubNodes[0].addedVol(Triangles[i]))
                    {
                        minArea = SubNodes[0].addedVol(Triangles[i]);
                        minId = i;
                        selected = 0;
                    }

                    if (minArea > SubNodes[1].addedVol(Triangles[i]))
                    {
                        minArea = SubNodes[1].addedVol(Triangles[i]);
                        minId = i;
                        selected = 1;
                    }
                }

                SubNodes[selected].IncreaseVol(Triangles[minId]);
                SubNodes[selected].Triangles.Add(Triangles[minId]);
                Triangles.RemoveAt(minId);
            }

            //GetMinPoint();
            //var right = //GetMaxPoint();
        }

        public Triangle GetMinPoint()
        {
            var minId = 0;
            var min = 0.0;
            for (int i = 0; i < Triangles.Count; i++)
            {
                if (i == 0 || min > Triangles[i].Weight)
                {
                    min = Triangles[i].Weight;
                    minId = i;
                }
            }

            var res = new Triangle(Triangles[minId]);
            Triangles.RemoveAt(minId);
            return res;
        }

        public Triangle GetMaxPoint()
        {
            var maxId = 0;
            var max = 0.0;
            for (int i = 0; i < Triangles.Count; i++)
            {
                if (i == 0 || max < Triangles[i].Weight)
                {
                    max = Triangles[i].Weight;
                    maxId = i;
                }
            }
            var res = new Triangle(Triangles[maxId]);
            Triangles.RemoveAt(maxId);
            return res;
        }

        public Node(Point minPoint, Point maxPoint)
        {
            Triangles = new List<Triangle>();
            SubNodes = new List<Node>();
            _minPoint = new Point(minPoint);
            _maxPoint = new Point(maxPoint);
        }

        public Node(Point minPoint, Point maxPoint, Triangle triangle)
        {
            _minPoint = minPoint;
            _maxPoint = maxPoint;
            Triangles = new List<Triangle>();
            SubNodes = new List<Node>();
            Triangles.Add(triangle);
        }

        public float addedVol(Triangle p)
        {
            var copy = new Node(_minPoint, _maxPoint);
            var oldVol = copy.Volume;
            copy.IncreaseVol(p);
            return copy.Volume - oldVol;
        }

        public float Volume =>
            Math.Abs((_maxPoint.X - _minPoint.X) * (_maxPoint.Y - _minPoint.Y) * (_maxPoint.Z - _minPoint.Z));


        public void IncreaseVol(Triangle p)
        {
            GetSmallest(p.MinPoint);
            GetBiggest(p.MaxPoint);
        }

        private void GetBiggest(Point p)
        {
            if (_maxPoint.X < p.X)
            {
                _maxPoint.X = p.X;
            }

            if (_maxPoint.Y < p.Y)
            {
                _maxPoint.Y = p.Y;
            }

            if (_maxPoint.Z < p.Z)
            {
                _maxPoint.Z = p.Z;
            }
        }

        private void GetSmallest(Point p)
        {
            if (_minPoint.X > p.X)
            {
                _minPoint.X = p.X;
            }

            if (_minPoint.Y > p.Y)
            {
                _minPoint.Y = p.Y;
            }

            if (_minPoint.Z > p.Z)
            {
                _minPoint.Z = p.Z;
            }
        }

        public Node(Triangle triangle)
        {
            _maxPoint = triangle.MaxPoint;
            _minPoint = triangle.MinPoint;
            Triangles = new List<Triangle>();
            SubNodes = new List<Node>();
            Triangles.Add(triangle);
        }
    }
}