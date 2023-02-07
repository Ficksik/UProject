namespace Core
{
    public class SceneContainer : Container
    {
        public override void Init()
        {
            BindChildren();
            InitializeChildren();
        }

        private void OnDestroy()
        {
            Dispose();
        }
    }
}
