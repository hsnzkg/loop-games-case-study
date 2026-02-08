using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.MonoBehaviour;
using Project.Scripts.GameState;
using Project.Scripts.GameState.States;
using Project.Scripts.Singleton;
using UnityEngine;

namespace Project.Scripts.Utility
{
    public class MonoBehaviourBridge : MonoBehaviourSingleton<MonoBehaviourBridge>
    {
        private EUpdate m_update;
        private EFixedUpdate m_fixedUpdate;
        protected override void OnAwake()
        {
            base.OnAwake();
            m_update = new EUpdate(0f);
            m_fixedUpdate = new EFixedUpdate(Time.fixedDeltaTime);
        }
        
        private void Update()
        {
            m_update.SetDelta(Time.deltaTime);
            EventBus<EUpdate>.Raise(m_update);
        }

        private void FixedUpdate()
        {
            EventBus<EFixedUpdate>.Raise(m_fixedUpdate);
        }

        private void OnApplicationQuit()
        {
            GameStateManager.RequestStateChange<Quit>();
        }
    }
}