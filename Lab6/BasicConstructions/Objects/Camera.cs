using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Lab6.BasicConstructions;
using Lab6.BasicConstructions.Mesh;
using Lab6.BasicConstructions.Objects;
using Lab6.BasicConstructions.RTree;
using Object = Lab6.BasicConstructions.Objects.Object;

namespace Lab6
{
    public class Camera : Object
    {
        private float _fov;
        private int _resolutionX;
        private int _resolutionY;
        private Vector _direction;
        public Scene Scene;
        private float _progress;
        public Camera(float fov, int resolutionX, int resolutionY, float x, float y, float z, Vector direction)
        {
            _fov = fov;
            _resolutionX = resolutionX;
            _resolutionY = resolutionY;
            Position = new Point(x, y, z);
            _direction = direction;
            float len = (float)Math.Sqrt(_direction.X * _direction.X + _direction.Y * _direction.Y + _direction.Z * _direction.Z);
            _direction.X /= len;
            _direction.Y /= len;
            _direction.Z /= len;
        }

        public List<Color> GetColors(Node head)
        {
            List<Vector> directions = new List<Vector>();
            List<Color> colors = new List<Color>();
            float deltaX = (float) (_fov / _resolutionX / 180 * Math.PI);
            float deltaY = (float) (_fov / _resolutionY / 180 * Math.PI);
            GetAngles(out float tota, out float fi);
            _progress = 0;
            ProgressAsync();
            for (int i = -_resolutionX / 2; i <= _resolutionX / 2; i++)
            {
                for (int j = -_resolutionY / 2; j <= _resolutionY / 2; j++)
                {
                    if (i == 0 || j == 0 )continue;
                    float dtota = tota - deltaX * i;
                    if (dtota > Math.PI) dtota = 2 * (float)Math.PI - dtota;
                    float dfi = fi + deltaY * j;
                    Vector dir = new Vector((float) (Math.Sin(dtota) * Math.Cos(dfi)),
                        (float) (Math.Sin(dtota) * Math.Sin(dfi)), (float)Math.Cos(dtota));
                    directions.Add(dir);
                    Color curr = Scene.Background;
                    colors.Add(curr);
                    float minDist = 100000;
                    float currDist = 0;
                    var currRay = new Ray(dir, Position);
                    var triangle = new List<Triangle>();
                    currRay.NewIntersect(head, triangle);
                    //Console.WriteLine(triangle.Count+" "+ Scene.MainObject.Triangles.Count);
                    foreach (var t in triangle)
                    {
                        Point intersect = new Point(0,0,0);
                        if (currRay.Intersects(t, ref currDist, ref intersect))
                        {
                            curr = Scene.MainObject.Color * Light.MultipleShade(intersect, t, Scene);
                        }
                        else
                        {
                            curr = Scene.Background;
                        }
                        if (curr != Scene.Background && currDist <= minDist)
                        {
                            colors[^1] = curr;
                            minDist = currDist;
                            //break;
                        }
                    }

                    _progress++;
                    
                }
            }
            Console.Clear();
            Console.WriteLine("100,00%");
            return colors;
        }
        
        private async void ProgressAsync()
        {
            await Task.Run(() => Progress());
        }

        private void Progress()
        {
            while (_progress < _resolutionX * _resolutionY)
            {
                Console.Clear();
                 Console.WriteLine($"{_progress / (_resolutionX * _resolutionY) * 100:f2} %");
                Thread.Sleep(1000);
            }
        }

        private void GetAngles(out float tota, out float fi)
        {
            tota = (float)Math.Atan((Math.Sqrt(_direction.X * _direction.X + _direction.Y * _direction.Y)) / _direction.Z);
            if (!(tota >= 0 && tota <= Math.PI))
                tota = (float) Math.Acos(_direction.Z);
            fi = (float)Math.Asin(_direction.Y / Math.Sin(Math.Sqrt(_direction.X * _direction.X + _direction.Y * _direction.Y)));
            if (Math.Sqrt(_direction.X * _direction.X + _direction.Y * _direction.Y) <= 0.00001) fi = 0;
            
            #region if vector is collinear OX, OY or OZ
            if (_direction.X >= 0.99999999)
            {
                fi = 0;
                tota = (float)Math.PI / 2;
            }
            if (_direction.X <= -0.9999999)
            {
                fi = (float)Math.PI;
                tota = (float)Math.PI / 2;
            }
            if (_direction.Y >= 0.99999999)
            {
                fi = (float)Math.PI / 2;
                tota = (float)Math.PI / 2;
            }
            if (_direction.Y <= -0.9999999)
            {
                fi = 3 * (float)Math.PI / 2;
                tota = (float)Math.PI / 2;
            }
            if (_direction.Z >= 0.99999999)
            {
                fi = 0;
                tota = 0;
            }
            if (_direction.Z <= -0.99999999)
            {
                fi = -(float)Math.PI/2;
                tota = (float)Math.PI;
            }
            #endregion
            
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
                
                var listOfPixels = GetColors(Scene.MainObject.Head);
                /*Console.WriteLine(">>>>>>>><<<<<<<<");
                Console.WriteLine(bmpByte);
                Console.WriteLine();
                Console.WriteLine(">>>>>>>><<<<<<<<");*/
                var j = 54;
                foreach (var pixel in listOfPixels)
                {
                    bmpByte[j++] = (byte) (pixel.B * 255);
                    bmpByte[j++] = (byte) (pixel.G * 255);
                    bmpByte[j++] = (byte) (pixel.R * 255);
                }

                bmpByte[j] = 0;

                binaryWriter.Write(bmpByte);
            }
        }
    }
}