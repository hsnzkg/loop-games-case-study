using Project.Scripts.GameState;
using Project.Scripts.Utility;
using UnityEngine;


namespace Project.Scripts.Bootstrap
{
    public class GameBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            Application.targetFrameRate = -1;
            MonoBehaviourBridge.Initialize();
            GameStateManager.Initialize();
        }
    }
}