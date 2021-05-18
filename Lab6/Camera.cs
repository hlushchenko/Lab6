using System;
using System.Collections.Generic;

namespace Lab6
{
    public class Camera : Object
    {
        private List<Ray> _rays;
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

        public void GetRays()
        {
            List<Ray> _rays = new List<Ray>();
            float delta = (float) (_fov / 180 * Math.PI) / (_resolutionX * _resolutionY);
            float tota = (float) Math.Acos(_direction.Z);
            float fi = (float) Math.Asin(_direction.Y / Math.Sin(tota));
            for (int i = 0; i < _resolutionX; i++)
            {
                for (int j = 0; j < _resolutionY; j++)
                {
                    float dtota = tota - delta * (i - _resolutionX / 2);
                    float dfi = fi + delta * (j - _resolutionY / 2);
                    Vector dir = new Vector((float) (Math.Sin(dtota) * Math.Cos(dfi)),
                        (float) (Math.Sin(dtota) * Math.Sin(dfi)), (float) Math.Cos(dtota));
                    _rays.Add(new Ray(dir, Position));
                }
            }
        }
    }
}