namespace Lab6.BasicConstructions.Mesh
{
    public class Triangle
    {
        public Point[] Verticles;
        public Vector Normal;
        public Triangle(params Point[] verticles)
        {
            Verticles = verticles;
        }
        
        public override string ToString()
        {
            return $"[ {Verticles[0]} ; {Verticles[1]} ; {Verticles[2]} ], n{Normal}";
        }
    }
}