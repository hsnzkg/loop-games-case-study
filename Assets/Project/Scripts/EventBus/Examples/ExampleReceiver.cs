using Project.Scripts.EventBus.Examples.Events;
using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.EventBus.Examples
{
    public class ExampleReceiver : MonoBehaviour
    {
        private EventBind<EExampleEvent> _ebExample;

        private void Awake()
        {
            _ebExample = new EventBind<EExampleEvent>(OnUpdate);
        }

        private void OnEnable()
        {
            EventBus<EExampleEvent>.Register(_ebExample);
        }
        
        private void OnDisable()
        {
            EventBus<EExampleEvent>.Unregister(_ebExample);
        }

        private void OnUpdate(EExampleEvent e)
        {
            Debug.Log($"[Receiver] Data : {e.Data} - Frame : {Time.frameCount}");
        }
    }
}