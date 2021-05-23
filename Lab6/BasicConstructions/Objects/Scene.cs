using System.Collections.Generic;

namespace Lab6.BasicConstructions.Objects
{
    public class Scene
    {
        public Camera MainCamera;
        public Color Background;
        public List<Light> Light;
        public Color EmbientColor { get; set; }
        public Mesh.Mesh MainObject;
        
        public Scene(Camera mainCamera, Light light, Mesh.Mesh mainObject)
        {
            EmbientColor = Color.Black;
            Light = new List<Light>();
            MainCamera = mainCamera;
            Light.Add(light);
            MainCamera.Scene = this;
            Light[0].Scene = this;
            Background = Color.Green;
            MainObject = mainObject;
        }

        public void AddLight(Light light)
        {
            Light.Add(light);
        }
    }
}