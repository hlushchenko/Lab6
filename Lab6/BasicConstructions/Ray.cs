using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions.Objects;

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
        
        ///<summary>Колір полігону, що перетинає промінь</summary>
        public Color GetColor(Triangle triangle, Scene scene, ref float distance)
        {
            const double eps = 0.0000001;
            Vector edge1 = new Vector(triangle.Verticles[0], triangle.Verticles[1]);
            Vector edge2 = new Vector(triangle.Verticles[0], triangle.Verticles[2]);
            Vector h = Direction.CrossProduct(edge2);
            float a = edge1.DotProduct(h);
            if (a > -eps && a < eps)
            {
                return scene.Background;
            }
            
            float f = 1f / a;
            Vector s = new Vector(triangle.Verticles[0], Origin);
            float u = f * s.DotProduct(h);
            
            if (u < 0f || u > 1f)
            {
                return scene.Background;
            }

            Vector q = s.CrossProduct(edge1);
            float v = f * Direction.DotProduct(q);

            if (v < 0f || u + v > 1f)
            {
                return scene.Background;
            }

            float t = f * edge2.DotProduct(q);
            if (t > eps)
            {
                Point intersect = Origin + Direction * t;
                distance = new Vector(Origin, intersect).Length();
                return scene.Light.Shade(intersect, triangle);
                //return Color.White;
            }
            return scene.Background;
        }
    }
}