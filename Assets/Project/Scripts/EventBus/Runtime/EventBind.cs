using System;

namespace Project.Scripts.EventBus.Runtime
{
    public class EventBind<T> where T : IEvent
    {
        private Action _handlerNoArgs;
        private Action<T> _handlerWithArgs;
        private readonly int m_channel;
        private readonly EventPriority m_priority;

        internal int GetChannel()
        {
            return m_channel;
        }

        internal EventPriority GetPriority()
        {
            return m_priority;
        }

        public EventBind(Action handler, int channel = 0, EventPriority priority = EventPriority.MONITOR)
        {
            _handlerNoArgs = handler;
            _handlerWithArgs = null;
            m_channel = channel;
            m_priority = priority;
        }

        public EventBind(Action<T> handler, int channel = 0, EventPriority priority = EventPriority.MONITOR)
        {
            _handlerWithArgs = handler;
            _handlerNoArgs = null;
            m_channel = channel;
            m_priority = priority;
        }

        public void Invoke(T @event)
        {
            _handlerNoArgs?.Invoke();
            _handlerWithArgs?.Invoke(@event);
        }

        public void Add(Action handler)
        {
            if(handler == null) return;
            _handlerNoArgs += handler;
        }

        public void Remove(Action handler)
        {
            if(handler == null) return;
            _handlerNoArgs -= handler;
        }

        public void Add(Action<T> handler)
        {
            if(handler == null) return;
            _handlerWithArgs += handler;
        }

        public void Remove(Action<T> handler)
        {
            if(handler == null) return;
            _handlerWithArgs -= handler;
        }
    }
}