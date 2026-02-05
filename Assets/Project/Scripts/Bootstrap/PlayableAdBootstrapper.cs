using Luna.Unity;
using Project.Scripts.Constants;
using Project.Scripts.GameStateManagement;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Bootstrap
{
    public class PlayableAdBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            Playable.InstallFullGame(StoreConstants.appStoreLink,StoreConstants.playStoreLink);
            
            Application.targetFrameRate = 60;
            MonoBehaviourBridge.Initialize();
            GameStateManager.Initialize();
        }
    }
}