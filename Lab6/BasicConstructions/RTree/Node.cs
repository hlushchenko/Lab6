using System;
using System.Collections.Generic;
using System.Xml;
using Lab6.BasicConstructions.Mesh;

namespace Lab6.BasicConstructions.RTree
{
    public class Node
    {
        public Point _minPoint;

        public Point _maxPoint;

        //maxChildren>=8
        public const int maxChildren = 40;
        public const float acceptedPart = 0.7f;

        public List<Triangle> Triangles;
        public List<Node> SubNodes;

        public Node(Node node)
        {
            _minPoint = new Point(node._minPoint);
            _maxPoint = new Point(node._maxPoint);
            Triangles = new List<Triangle>(node.Triangles);
            SubNodes = new List<Node>(node.SubNodes);
        }

        public void Insert(Triangle t)
        {
            IncreaseVol(t);

            if (SubNodes.Count != 0)
            {
                int minId = 0;
                float minFitVol = 0f;
                for (int i = 0; i < SubNodes.Count; i++)
                {
                    var tempFitVol = 0f;
                    for (int j = 0; j < SubNodes.Count; j++)
                    {
                        if (j == i)
                        {
                            continue;
                        }

                        tempFitVol += SubNodes[i].nodeWithTriangle(t).FitArea(SubNodes[j]);
                    }

                    if (i == 0 || tempFitVol < minFitVol)
                    {
                        minFitVol = tempFitVol;
                        minId = i;
                    }
                }

                if (minFitVol != 0)
                {
                    float minAddedVol = SubNodes[0].addedVol(t);

                    for (int i = 1; i < SubNodes.Count; i++)
                    {
                        if (minAddedVol > SubNodes[i].addedVol(t))
                        {
                            minId = i;
                            minAddedVol = SubNodes[i].addedVol(t);
                        }
                    }
                }

                SubNodes[minId].Insert(t);

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

        public float FitArea(Node node)
        {
            var left = Math.Max(node._minPoint.X, _minPoint.X);
            var top = Math.Min(node._maxPoint.Y, _maxPoint.Y);
            var right = Math.Min(node._maxPoint.X, _maxPoint.X);
            var bottom = Math.Max(node._minPoint.Y, _maxPoint.Y);
            var front = Math.Min(node._maxPoint.Z, _maxPoint.Z);
            var back = Math.Max(node._minPoint.Z, _minPoint.Z);

            var width = right - left;
            var height = top - bottom;
            var deep = front - back;

            if (width < 0 || height < 0 || deep < 0)
            {
                return 0;
            }

            return width * height * deep;
        }

        public void Split()
        {
            if (Triangles.Count == 0)
            {
                Console.WriteLine("Nothing to split");
                return;
            }

            foreach (var triarray in GetPoints())
            {
                SubNodes.Add(new Node(triarray[0].MinPoint, triarray[0].MaxPoint, triarray[0]));
                SubNodes.Add(new Node(triarray[1].MinPoint, triarray[1].MaxPoint, triarray[1]));
            }

            while (Triangles.Count != 0)
            {
                Insert(Triangles[0]);
                Triangles.RemoveAt(0);
            }
        }
        /*var selected = 0;
        var minId = 0;
        var minArea = -1.0;
        for (int i = 0; i < Triangles.Count; i++)
        {
        */
        /* if (i == 0 || minArea > SubNodes[0].addedVol(Triangles[i]))
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
         }*/


        // SubNodes[selected].IncreaseVol(Triangles[minId]);
        // SubNodes[selected].Triangles.Add(Triangles[minId]);
        // Triangles.RemoveAt(minId);


        //GetMinPoint();
        //var right = //GetMaxPoint();

        public Node nodeWithTriangle(Triangle triangle)
        {
            var copy = new Node(this);
            copy.Triangles.Add(triangle);
            copy.IncreaseVol(triangle);
            return copy;
        }

        // 1 - x+y+z
        // 2 - x-y+z
        // 3 - x+y-z
        // 4 - x-y-z

        public List<Triangle[]> GetPoints()
        {
            var minId = new int[4];
            var maxId = new int[4];
            var dif = new float[4];
            Operation op1, op2;
            op1 = Add;
            op2 = Substract;
            GetPointWeight(op1, op1, out minId[0], out maxId[0], out dif[0]);
            GetPointWeight(op1, op2, out minId[1], out maxId[1], out dif[1]);
            GetPointWeight(op2, op1, out minId[2], out maxId[2], out dif[2]);
            GetPointWeight(op2, op2, out minId[3], out maxId[3], out dif[3]);
            var maxWeight = dif[0];
            var maxWeightId = 0;
            for (int i = 1; i < 4; i++)
            {
                if (maxWeight < dif[i])
                {
                    maxWeightId = i;
                    maxWeight = dif[i];
                }
            }

            var result = new List<Triangle[]>();
            result.Add(new[] {Triangles[minId[maxWeightId]], Triangles[maxId[maxWeightId]]});
            var deleteList = new List<int> {minId[maxWeightId], maxId[maxWeightId]};
            for (int i = 0; i < 4; i++)
            {
                if (i == maxWeightId)
                {
                    continue;
                }

                if (dif[i] > maxWeight * acceptedPart)
                {
                    result.Add(new[] {Triangles[minId[i]], Triangles[maxId[i]]});
                }

                deleteList.Add(minId[i]);
                deleteList.Add(maxId[i]);
            }
            
            for (int i = deleteList.Count - 1; i >= 0; i--)
            {
                
                Triangles.RemoveAt(deleteList[i]);
            }

            return result;
        }

        public void GetPointWeight(Operation op1, Operation op2, out int minResId, out int maxResId,
            out float substrWeight)
        {
            minResId = 0;
            maxResId = 0;
            var min = 0.0f;
            var max = 0.0f;
            for (int i = 0; i < Triangles.Count; i++)
            {
                var tempWeight = Triangles[i].GetWeight(op1, op2);
                if (i == 0 || min > tempWeight)
                {
                    min = tempWeight;
                    minResId = i;
                }

                if (i == 0 || max < tempWeight)
                {
                    max = tempWeight;
                    maxResId = i;
                }
            }

            substrWeight = max - min;
        }

        public delegate float Operation(float a, float b);

        public float Add(float a, float b)
        {
            return a + b;
        }

        public float Substract(float a, float b)
        {
            return a - b;
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