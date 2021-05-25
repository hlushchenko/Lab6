using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions.Objects;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            //string resourcesPath = @"";
            int resx = 512;
            int resy = 512;
            Camera cum = new Camera(60, resx, resy, 1, 0, 0, new Vector(-1, 0, 0));
            Light lite1 = new Light(new Point(1, 1, 3), 6, Color.White);
            //Light lite2 = new Light(new Point(2, 0, 3), 6, new Color("#155799"));
            Mesh meh = new Mesh("D:/cow.obj", /*new Color("#bfbfbf")*/ Color.White);
            Scene mainScene = new Scene(cum, lite1, meh);
            //mainScene.AddLight(lite2);
            mainScene.Background = Color.Black;
           // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            mainScene.EmbientColor = new Color(0.1f, 0.1f, 0.1f);
            cum.Screenshot("D:/Sphere.bmp");
        }
    }
}