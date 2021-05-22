using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab6.BasicConstructions.RTree
{
    public class Parallelepiped : Node
    {
        private Point _minPoint;
        private Point _maxPoint;
        public List<Node> Children;

        public float Volume =>
            Math.Abs((_maxPoint.X - _minPoint.X) * (_maxPoint.Y - _minPoint.Y) * (_maxPoint.Z - _minPoint.Z));

        public Parallelepiped(Point minPoint, Point maxPoint)
        {
            _minPoint = minPoint;
            _maxPoint = maxPoint;
        }
        
        public float GetOverlap(Parallelepiped a, Parallelepiped b)
        {
            return Math.Abs(GetDifference(a._minPoint.X, a._maxPoint.X, b._minPoint.X, b._maxPoint.X)
                            * GetDifference(a._minPoint.Y, a._maxPoint.Y, b._minPoint.Y, b._maxPoint.Y)
                            * GetDifference(a._minPoint.Z, a._maxPoint.Z, b._minPoint.Z, b._maxPoint.Z));
        }

        private float GetDifference(float a1, float a2, float b1, float b2)
        {
            if (a1 <= b1 && b1 <= a2)
            {
                if (b2 <= a2)
                    return b2 - b1;
                return a2 - b1;
            }
            else if (b1 <= a1 && a1 <= b2)
            {
                if (a2 <= b2)
                    return a2 - a1;
                return b2 - a1;
            }

            return 0;
        }
    }
}

}