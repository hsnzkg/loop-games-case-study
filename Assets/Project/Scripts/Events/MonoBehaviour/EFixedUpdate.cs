using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.Events.MonoBehaviour
{
    public struct EFixedUpdate : IEvent
    {
        public readonly float Delta;
        
        public EFixedUpdate(float delta)
        {
            Delta = delta;  
        }
    }
}