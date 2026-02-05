using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.EventBus.Runtime
{
    public static class EventBus<T> where T : IEvent
    {
        private static readonly List<EventBind<T>> _bindings = new List<EventBind<T>>();
        private static readonly HashSet<EventBind<T>> _pendingAdds = new HashSet<EventBind<T>>();
        private static readonly HashSet<EventBind<T>> _pendingRemovals = new HashSet<EventBind<T>>();

        private static PendingFlags _pendingFlags;
        private static int _raiseDepth;
        private static int _count;

        public static void Register(EventBind<T> bind)
        {
            if (bind == null) return;

            if (_raiseDepth > 0)
            {
                _pendingAdds.Add(bind);
                _pendingRemovals.Remove(bind);
                _pendingFlags |= PendingFlags.HAS_ADDS;
                return;
            }

            RegisterImmediate(bind);
        }

        public static void Unregister(EventBind<T> bind)
        {
            if (bind == null) return;

            if (_raiseDepth > 0)
            {
                _pendingRemovals.Add(bind);
                _pendingAdds.Remove(bind);
                _pendingFlags |= PendingFlags.HAS_REMOVALS;
                return;
            }

            UnregisterImmediate(bind);
        }

        private static void RegisterImmediate(EventBind<T> bind)
        {
            if (_bindings.Contains(bind))
            {
                Debug.LogWarning("[EventBus] Same bind registered twice.");
                return;
            }

            int index = _bindings.FindIndex(b => b.GetPriority().CompareTo(bind.GetPriority()) > 0);

            if (index >= 0)
            {
                _bindings.Insert(index, bind);
            }
            else
            {
                _bindings.Add(bind);
            }

            _count++;
        }

        private static void UnregisterImmediate(EventBind<T> bind)
        {
            _bindings.Remove(bind);
            _count--;
        }

        public static void Raise(T @event)
        {
            if (@event == null)
            {
                Debug.LogError($"[EventBus] Null event : {typeof(T).Name}");
                return;
            }

            _raiseDepth++;
            for (int i = 0; i < _count; i++)
            {
                try
                {
                    _bindings[i].Invoke(@event);
                }
                catch (Exception e)
                {
                    Debug.LogError($"Error invoking event handler -> Bind : {_bindings[i].GetType().Name}\n{e}");
                }
            }

            _raiseDepth--;

            if (_raiseDepth == 0)
            {
                FlushPending();
            }
        }

        private static void FlushPending()
        {
            if ((_pendingFlags & PendingFlags.HAS_REMOVALS) != 0)
            {
                foreach (EventBind<T> bind in _pendingRemovals)
                {
                    UnregisterImmediate(bind);
                }

                _pendingRemovals.Clear();
                _pendingFlags &= ~PendingFlags.HAS_REMOVALS;
            }

            if ((_pendingFlags & PendingFlags.HAS_ADDS) == 0) return;
            {
                foreach (EventBind<T> bind in _pendingAdds)
                {
                    RegisterImmediate(bind);
                }

                _pendingAdds.Clear();
                _pendingFlags &= ~PendingFlags.HAS_ADDS;
            }
        }

        public static void Dispose()
        {
            _bindings.Clear();
            _pendingAdds.Clear();
            _pendingRemovals.Clear();
            _raiseDepth = 0;
            _pendingFlags = PendingFlags.NONE;

            Debug.Log($"[EventBus] Cleared bindings for {typeof(T).Name}");
        }

        public static void DisposeWithCenter()
        {
            typeof(T).DisposeWithCenter();
        }

        [Flags]
        private enum PendingFlags : byte
        {
            NONE = 0,
            HAS_ADDS = 1 << 0,
            HAS_REMOVALS = 1 << 1
        }
    }
}