using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.Events.MonoBehaviour
{
    public struct EUpdate : IEvent
    {
        public float Delta;
        
        public EUpdate(float delta)
        {
            Delta = delta;  
        }

        public void SetDelta(float delta)
        {
            Delta = delta;
        }
    }
}