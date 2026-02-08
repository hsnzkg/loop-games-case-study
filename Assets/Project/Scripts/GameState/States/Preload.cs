using Project.Scripts.EventBus.Runtime;
using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.GameState.States
{
    public class Preload : StateBase
    {
        protected override void OnEnter()
        {
            Application.targetFrameRate = 60;
            EventBusCenter.Initialize();
            StorageCenter.Initialize();
            MonoBehaviourBridge.Initialize();
            GameStateManager.RequestStateChange<Gameplay>();
        }

        protected override void OnExit()
        {
        }
    }
}