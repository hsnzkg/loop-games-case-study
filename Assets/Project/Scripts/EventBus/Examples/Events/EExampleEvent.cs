using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.EventBus.Examples.Events
{
    public struct EExampleEvent : IEvent
    {
        public readonly string Data;

        public EExampleEvent(string data)
        { 
            Data = data;
        }
    }
}