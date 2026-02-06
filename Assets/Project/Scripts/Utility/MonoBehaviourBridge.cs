using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.MonoBehaviour;
using Project.Scripts.Singleton;
using UnityEngine;

namespace Project.Scripts.Utility
{
    public class MonoBehaviourBridge : MonoBehaviourSingleton<MonoBehaviourBridge>
    {
        private EUpdate m_update;
        protected override void OnAwake()
        {
            base.OnAwake();
            m_update = new EUpdate(0f);
        }
        
        private void Update()
        {
            m_update.SetDelta(Time.deltaTime);
            EventBus<EUpdate>.Raise(m_update);
        }
    }
}