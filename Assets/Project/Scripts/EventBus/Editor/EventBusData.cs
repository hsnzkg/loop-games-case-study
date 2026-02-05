using System.Collections;

namespace Project.Scripts.EventBus.Editor
{
    public struct EventBusData
    {
        public readonly int Count;
        public readonly IList Snapshot;

        public EventBusData(IList bindings)
        {
            Snapshot = bindings;
            Count = bindings?.Count ?? 0;
        }

        public bool HasChanged(IList current)
        {
            if (current == null)
                return Snapshot != null;

            if (!ReferenceEquals(Snapshot, current))
                return true;

            return current.Count != Count;
        }
    }
}