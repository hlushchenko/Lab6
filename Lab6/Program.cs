using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions.Objects;

namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            /*int resx = 256;
            int resy = 256;
            Camera cum = new Camera(60, resx, resy, 3, 1f, 0, new Vector(-1, -0.1f, 0));
            Light cameraLight = new Light(new Point(3, 1, 0),1, Color.White);
            Light lite1 = new Light(new Point(-1, 1, 2), 6, Color.Red);
            Light lite2 = new Light(new Point(-1, 1, -2), 6, Color.Blue);
            Mesh meh = new Mesh("D:/shadowTester.obj", /*new Color("#bfbfbf")#1# Color.White);
            Scene mainScene = new Scene(cum, lite1, meh);
            mainScene.AddLight(lite2);
            mainScene.AddLight(cameraLight);
            mainScene.Background = Color.Black;
            // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            mainScene.EmbientColor = new Color("#0d0d0d");
            cum.Screenshot("D:/Sphere.bmp");*/
            
            int resx = 512;
            int resy = 512;
            Camera cum = new Camera(60, resx, resy, 0.75f, 0.2f, 0, new Vector(-1, 0, 0));
            Light lite1 = new Light(new Point(2, 0, -3), 6,new Color("#159957"));
            Light lite2 = new Light(new Point(2, 0, 3), 6, new Color("#155799"));
            Mesh meh = new Mesh("D:/cow.obj", Color.White);
            Scene mainScene = new Scene(cum, lite1, meh);
            mainScene.AddLight(lite2);
            mainScene.Background = Color.Black;
            // mainScene.Background = new Color(0.1f, 0.1f, 0.1f);
            //mainScene.EmbientColor = new Color(0.1f, 0.1f, 0.1f);
            cum.Screenshot("D:/Sphere.bmp");
        }
    }
}