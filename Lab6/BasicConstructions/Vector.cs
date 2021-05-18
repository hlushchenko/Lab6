namespace Lab6.BasicConstructions
{
    public class Vector
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        
        public Vector(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector(Point start, Point end)
        {
            X = end.X - start.X;
            Y = end.Y - start.Y;
            Z = end.Z - start.Z;
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
    }
}