using Project.Scripts.Singleton;
using UnityEngine;

namespace Project.Scripts.CameraManagement
{
    public class CameraFollowSystem : MonoBehaviourSingleton<CameraFollowSystem>
    {
        private Transform m_target;
    }
}
