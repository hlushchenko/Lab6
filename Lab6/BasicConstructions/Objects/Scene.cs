namespace Lab6.BasicConstructions.Objects
{
    public class Scene
    {
        public Camera MainCamera;
        public Color Background;
        public Light Light;
        public Mesh.Mesh MainObject;
        
        public Scene(Camera mainCamera, Light light, Mesh.Mesh mainObject)
        {
            MainCamera = mainCamera;
            Light = light;
            MainCamera.Scene = this;
            Light.Scene = this;
            Background = Color.Green;
            MainObject = mainObject;
        }
    }
}