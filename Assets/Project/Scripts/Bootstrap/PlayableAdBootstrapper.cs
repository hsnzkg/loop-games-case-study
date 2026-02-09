using Luna.Unity;
using Project.Scripts.Constants;
using Project.Scripts.GameState;

namespace Project.Scripts.Bootstrap
{
    public class PlayableAdBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            Playable.InstallFullGame(StoreConstants.appStoreLink,StoreConstants.playStoreLink);
            Analytics.LogEvent(Analytics.EventType.LevelStart);
            GameStateManager.Initialize();
        }
    }
}