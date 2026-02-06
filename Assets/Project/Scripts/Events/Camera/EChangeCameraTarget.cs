using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.Events.Camera
{
    public struct EChangeCameraTarget : IEvent
    {
        public readonly Transform Target;

        public EChangeCameraTarget(Transform target)
        {
            Target = target;
        }
    }
}