namespace Lab6.BasicConstructions.Objects
{
    public class Scene
    {
        public Camera MainCamera;
        public Color Background;
        public Light Light;
        
        public Scene(Camera mainCamera)
        {
            MainCamera = mainCamera;
            Background = Color.Black;
        }
    }
}