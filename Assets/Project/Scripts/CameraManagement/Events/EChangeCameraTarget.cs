using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.CameraManagement.Events
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