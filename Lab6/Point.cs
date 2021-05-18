namespace Lab6
{
    public class Point
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }


        public Point(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Translate(float x, float y, float z)
        {
            X += x;
            Y += y;
            Z += z;
        }
    }
}