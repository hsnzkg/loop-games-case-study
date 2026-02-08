using Project.Scripts.GameState;
using Project.Scripts.Utility;
using UnityEngine;


namespace Project.Scripts.Bootstrap
{
    public class GameBootstrapper : IBootstrapper
    {
        public void Bootstrap()
        {
            Application.targetFrameRate = 60;
            MonoBehaviourBridge.Initialize();
            GameStateManager.Initialize();
        }
    }
}