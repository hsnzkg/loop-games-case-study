using Luna.Unity;
using Project.Scripts.Constants;
using Project.Scripts.GameState;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Bootstrap
{
    public class PlayableAdBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            Application.targetFrameRate = -1;
            Playable.InstallFullGame(StoreConstants.appStoreLink,StoreConstants.playStoreLink);
            MonoBehaviourBridge.Initialize();
            GameStateManager.Initialize();
        }
    }
}