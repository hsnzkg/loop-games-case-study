using Project.Scripts.EventBus.Examples.Events;
using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.EventBus.Examples
{
    public class ExampleReceiver : MonoBehaviour
    {
        private EventBind<EExampleEvent> m_ebExample;

        private void Awake()
        {
            m_ebExample = new EventBind<EExampleEvent>(OnUpdate);
        }

        private void OnEnable()
        {
            EventBus<EExampleEvent>.Register(m_ebExample);
        }
        
        private void OnDisable()
        {
            EventBus<EExampleEvent>.Unregister(m_ebExample);
        }

        private void OnUpdate(EExampleEvent e)
        {
            Debug.Log($"[Receiver] Data : {e.Data} - Frame : {Time.frameCount}");
        }
    }
}