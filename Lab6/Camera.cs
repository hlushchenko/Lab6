using System;
using System.Collections.Generic;
using Lab6.BasicConstructions;

namespace Lab6
{
    public class Camera : Object
    {
        public List<Ray> _rays;
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
            _rays = new List<Ray>();
            float deltaX = (float) (_fov / _resolutionX  / 180 * Math.PI);
            float deltaY = (float) (_fov / _resolutionY  / 180 * Math.PI);
            float tota = (float) Math.Acos(_direction.Z);
            float fi = (float) Math.Asin(_direction.Y / Math.Sin(tota));
            for (int i = - _resolutionX / 2; i < _resolutionX/2; i++)
            {
                for (int j = - _resolutionY / 2; j < _resolutionY/2; j++)
                {
                    float dtota = tota + deltaX * i;
                    float dfi = fi + deltaY * j;
                    Vector dir = new Vector((float) (Math.Sin(dtota) * Math.Cos(dfi)),
                        (float) (Math.Sin(dtota) * Math.Sin(dfi)), (float) Math.Cos(dtota));
                    _rays.Add(new Ray(dir, Position));
                }
            }
        }
    }
}