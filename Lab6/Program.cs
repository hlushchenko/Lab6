using System;


namespace Lab6
{
    class Program
    {
        static void Main(string[] args)
        {
            Camera cum = new Camera(60, 6, 8, 0, 0, 0, new Vector((float) 0.577, (float) 0.577, (float) 0.577));
            cum.GetRays();
        }
    }
}