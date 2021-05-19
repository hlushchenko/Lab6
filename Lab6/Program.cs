using System;
using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;


namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        { 
            var m = new Mesh(new Ngon[]{});
            m.Parse(@"C:\Users\Doncr\Desktop\KPI\ОП6\cube.obj");
            Console.WriteLine("Triangles:");
            foreach (var t in m.Triangles)
            {
                Console.WriteLine(">"+t);
            }
        }
    }
}