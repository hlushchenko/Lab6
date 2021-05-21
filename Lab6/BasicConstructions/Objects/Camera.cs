using System;
using System.Collections.Generic;
using System.IO;
using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;

namespace Lab6
{
    public class Camera : BasicConstructions.Objects.Object
    {
        private float _fov;
        private int _resolutionX;
        private int _resolutionY;
        private Vector _direction;

        public Camera(float fov, int resolutionX, int resolutionY, float x, float y, float z, Vector direction)
        {
            _fov = fov;
            _resolutionX = resolutionX;
            _resolutionY = resolutionY;
            Position = new Point(x, y, z);
            _direction = direction;
        }

        public List<Color> GetColors(Triangle[] triangle)
        {
            List<Color> colors = new List<Color>();
            float deltaX = (float) (_fov / _resolutionX / 180 * Math.PI);
            float deltaY = (float) (_fov / _resolutionY / 180 * Math.PI);
            float tota = (float) Math.Acos(_direction.Z);
            float fi = (float) Math.Asin(_direction.Y / Math.Sin(tota));
            for (int i = -_resolutionX / 2; i < _resolutionX / 2; i++)
            {
                for (int j = -_resolutionY / 2; j < _resolutionY / 2; j++)
                {
                    float dtota = tota - deltaX * i;
                    float dfi = fi + deltaY * j;
                    Vector dir = new Vector((float) (Math.Sin(dtota) * Math.Cos(dfi)),
                        (float) (Math.Sin(dtota) * Math.Sin(dfi)), (float) Math.Cos(dtota));
                    Color curr = Color.Black;
                    colors.Add(curr);
                    foreach (var t in triangle)
                    {
                        curr = new Ray(dir, Position).GetColor(t);
                        if (curr != Color.Black)
                        {
                            colors[^1] = curr;
                            break;
                        }
                    }
                }
            }

            return colors;
        }

        public void Screenshot(string filename)
        {
            using (BinaryWriter binaryWriter = new BinaryWriter(new FileStream(filename, FileMode.Truncate)))
            {
                byte[] bmpByte = new byte[_resolutionX * _resolutionY * 3 + 55];

                //BM
                bmpByte[0] = 66;
                bmpByte[1] = 77;

                //sizeOfFile
                byte[] bytes = BitConverter.GetBytes(_resolutionX * _resolutionY * 3 + 55);
                for (int i = 2; i < 6; i++)
                {
                    bmpByte[i] = bytes[i - 2];
                }

                //const
                bmpByte[6] = 0;
                bmpByte[7] = 0;
                bmpByte[8] = 0;
                bmpByte[9] = 0;

                bmpByte[10] = 54;
                bmpByte[11] = 0;
                bmpByte[12] = 0;
                bmpByte[13] = 0;

                bmpByte[14] = 40;
                bmpByte[15] = 0;
                bmpByte[16] = 0;
                bmpByte[17] = 0;

                //width + height

                bytes = BitConverter.GetBytes(_resolutionX);
                for (int i = 18; i < 22; i++)
                {
                    bmpByte[i] = bytes[i - 18];
                }

                bytes = BitConverter.GetBytes(_resolutionY);
                for (int i = 22; i < 26; i++)
                {
                    bmpByte[i] = bytes[i - 22];
                }

                bmpByte[26] = 1;
                bmpByte[27] = 0;

                bmpByte[28] = 24;
                for (int i = 29; i < 55; i++)
                {
                    bmpByte[i] = 0;
                }

                //GetColors() - тут будуть пікселі
                var listOfPixels = GetColors(new Triangle(new Point(5, 1, 0), new Point(5, -1, -1), new Point(5, 0, 1)));
                /*Console.WriteLine(">>>>>>>><<<<<<<<");
                Console.WriteLine(bmpByte);
                Console.WriteLine();
                Console.WriteLine(">>>>>>>><<<<<<<<");*/
                var j = 54;
                foreach (var pixel in listOfPixels)
                {
                    bmpByte[j++] = (byte) (pixel.R * 255);
                    bmpByte[j++] = (byte) (pixel.G * 255);
                    bmpByte[j++] = (byte) (pixel.B * 255);
                }

                bmpByte[j] = 0;

                binaryWriter.Write(bmpByte);
            }
        }
    }
}