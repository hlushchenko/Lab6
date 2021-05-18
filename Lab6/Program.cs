using System;
using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;


namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Ngon m = new Ngon(new Point[]
            {
                new(0, 0, 0),
                new(-1, 1, 0),
                new(1, 2, 0),
                new(2, 1, 0),
                new(1, 0, 0),
            });
            foreach (var triangle in m.Triangulate())
            {
                Console.WriteLine(triangle);
            }
        }
    }
}