using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.Events.Scratch
{
    public struct EScratch : IEvent
    {
        public readonly Vector2 Position;

        public EScratch(Vector2 position)
        {
            Position = position;
        }
    }
}