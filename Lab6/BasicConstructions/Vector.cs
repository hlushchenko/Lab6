using System;

namespace Lab6.BasicConstructions
{
    public class Vector
    {
        public static Vector Zero => new (0, 0, 0);
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }

        public Vector(Point start, Point end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
            Z = end.Z - start.Z;
        }

        public Vector(Vector src)
        {
            X = src.X;
            Y = src.Y;
            Z = src.Z;
        }
        
        public float Length()
        {
            return MathF.Sqrt(MathF.Pow(X, 2) + MathF.Pow(Y, 2) + MathF.Pow(Z, 2));
        }

        ///<summary>Векторний добуток векторів</summary>
        public Vector CrossProduct(Vector vector)
        {
            float i = Y * vector.Z - Z * vector.Y;
            float j = Z * vector.X - X * vector.Z;
            float k = X * vector.Y - Y * vector.X;
            return new Vector(i, j, k);
        }
        
        ///<summary>Скалярний добуток векторів</summary>
        public float DotProduct(Vector vector)
        {
            return X * vector.X + Y * vector.Y + Z * vector.Z;
        }
        ///<summary>Косинус кута між векторами</summary>
        public float AngleCos(Vector vector)
        {
            return DotProduct(vector) / (Length() * vector.Length());
        }

        #region Arythmetic operations
        public static Vector operator *(Vector vector, float mult)
        {
            return new(vector.X * mult, vector.Y * mult, vector.Z * mult);
        }
        
        public static Vector operator /(Vector vector, float div)
        {
            return new(vector.X / div, vector.Y / div, vector.Z / div);
        }
        
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new(vector1.X + vector2.X, vector1.Y + vector2.Y, vector1.Z + vector2.Z);
        }

        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new(vector1.X - vector2.X, vector1.Y - vector2.Y, vector1.Z - vector2.Z);
        }
        #endregion
        
    }
}