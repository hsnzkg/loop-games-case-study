using Project.Scripts.GameState;


namespace Project.Scripts.Bootstrap
{
    public class GameBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            GameStateManager.Initialize();
        }
    }
}