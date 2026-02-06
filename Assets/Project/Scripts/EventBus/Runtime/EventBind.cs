using System;

namespace Project.Scripts.EventBus.Runtime
{
    public class EventBind<T> where T : IEvent
    {
        private Action m_handlerNoArgs;
        private Action<T> m_handlerWithArgs;
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
            m_handlerNoArgs = handler;
            m_handlerWithArgs = null;
            m_channel = channel;
            m_priority = priority;
        }

        public EventBind(Action<T> handler, int channel = 0, EventPriority priority = EventPriority.MONITOR)
        {
            m_handlerWithArgs = handler;
            m_handlerNoArgs = null;
            m_channel = channel;
            m_priority = priority;
        }

        public void Invoke(T @event)
        {
            m_handlerNoArgs?.Invoke();
            m_handlerWithArgs?.Invoke(@event);
        }

        public void Add(Action handler)
        {
            if(handler == null) return;
            m_handlerNoArgs += handler;
        }

        public void Remove(Action handler)
        {
            if(handler == null) return;
            m_handlerNoArgs -= handler;
        }

        public void Add(Action<T> handler)
        {
            if(handler == null) return;
            m_handlerWithArgs += handler;
        }

        public void Remove(Action<T> handler)
        {
            if(handler == null) return;
            m_handlerWithArgs -= handler;
        }
    }
}