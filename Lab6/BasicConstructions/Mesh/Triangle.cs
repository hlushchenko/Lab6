namespace Lab6.BasicConstructions.Mesh
{
    public class Triangle
    {
        public Point[] Verticles;

        public Triangle(params Point[] verticles)
        {
            Verticles = verticles;
        }
    }
}