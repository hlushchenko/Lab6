using System;
using System.Collections.Generic;
using System.ComponentModel;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions.RTree;

namespace Lab6.BasicConstructions
{
    public class Ray
    {
        public Vector Direction;
        public Point Origin;

        public Ray(Vector direction, Point origin)
        {
            Direction = direction;
            Origin = origin;
        }

        public Point IntersectsPlain(string type, float Value)
        {
            float x, y, z;
            switch (type)
            {
                case "x":
                    y = (Direction.Y * Value - Direction.Y * Origin.X + Direction.X * Origin.Y) / Direction.X;
                    z = (Direction.Z * Value - Direction.Z * Origin.X + Direction.X * Origin.Z) / Direction.X;
                    return new Point(Value, y, z);
                case "y":
                    x = (Direction.X * Value - Direction.X * Origin.Y + Direction.Y * Origin.X) / Direction.Y;
                    z = (Direction.Z * Value - Direction.Z * Origin.Y + Direction.Y * Origin.Z) / Direction.Y;
                    return new Point(x, Value, z);
                case "z":
                    x = (Direction.X * Value - Direction.X * Origin.Z + Direction.Z * Origin.X) / Direction.Z;
                    y = (Direction.Y * Value - Direction.Y * Origin.Z + Direction.Z * Origin.Y) / Direction.Z;
                    return new Point(x, y, Value);
            }

            return null;
        }

        public void NewIntersect(Node node, List<Triangle> result)
        {
            if (node.SubNodes.Count != 0)
            {
                foreach (var sn in node.SubNodes)
                {
                    if (IntersectsCube(sn))
                    {
                        NewIntersect(sn, result);
                    }
                }
                
                return;
            }

            result.AddRange(node.Triangles);
        }

        public bool IntersectsCube(Node node)
        {
            var xMin = IntersectsPlain("x", node._minPoint.X);
            var xMax = IntersectsPlain("x", node._maxPoint.X);

            var yMin = IntersectsPlain("y", node._minPoint.Y);
            var yMax = IntersectsPlain("y", node._maxPoint.Y);

            var zMin = IntersectsPlain("z", node._minPoint.Z);
            var zMax = IntersectsPlain("z", node._maxPoint.Z);
            return (xMin.Y >= node._minPoint.Y && xMin.Y <= node._maxPoint.Y &&
                    xMin.Z >= node._minPoint.Z && xMin.Z <= node._maxPoint.Z) ||
                   (xMax.Y >= node._minPoint.Y && xMax.Y <= node._maxPoint.Y &&
                    xMax.Z >= node._minPoint.Z && xMax.Z <= node._maxPoint.Z) ||
                   (yMin.X >= node._minPoint.X && yMin.X <= node._maxPoint.X &&
                    yMin.Z >= node._minPoint.Z && xMin.Z <= node._maxPoint.Z) ||
                   yMax.X >= node._minPoint.X && yMin.X <= node._maxPoint.X &&
                   yMin.Z >= node._minPoint.Z && xMin.Z <= node._maxPoint.Z ||
                   (zMin.X >= node._minPoint.X && yMin.X <= node._maxPoint.X &&
                    zMin.Y >= node._minPoint.Y && xMin.Y <= node._maxPoint.Y) ||
                   (zMax.X >= node._minPoint.X && yMax.X <= node._maxPoint.X &&
                    zMax.Y >= node._minPoint.Y && xMax.Y <= node._maxPoint.Y);
        }

        ///<summary>Перетин променя та полігону</summary>
        public bool Intersects(Triangle triangle, ref float distance, ref Point intersect)
        {
            const double eps = 0.000001;
            Vector edge1 = new Vector(triangle.Verticles[0], triangle.Verticles[1]);
            Vector edge2 = new Vector(triangle.Verticles[0], triangle.Verticles[2]);
            Vector h = Direction.CrossProduct(edge2);
            float a = edge1.DotProduct(h);
            if (a > -eps && a < eps)
            {
                return false;
            }

            float f = 1f / a;
            Vector s = new Vector(triangle.Verticles[0], Origin);
            float u = f * s.DotProduct(h);

            if (u < 0f || u > 1f)
            {
                return false;
            }

            Vector q = s.CrossProduct(edge1);
            float v = f * Direction.DotProduct(q);

            if (v < 0f || u + v > 1f)
            {
                return false;
            }

            float t = f * edge2.DotProduct(q);
            if (t > eps)
            {
                intersect = Origin + Direction * t;
                distance = new Vector(Origin, intersect).Length();
                return true;
            }

            return false;
        }
    }
}