namespace Lab6.BasicConstructions
{
    public class Ray
    {
        public Vector Direction;
        public Point Base;

        public Ray(Vector direction, Point @base)
        {
            Direction = direction;
            Base = @base;
        }
    }
}