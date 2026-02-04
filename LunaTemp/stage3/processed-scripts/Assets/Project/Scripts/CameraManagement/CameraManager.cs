using UnityEngine;
using Cinemachine;

namespace Project.Scripts.CameraManagement
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraSettings m_cameraSettings;
        private CinemachineVirtualCamera m_virtualCamera;
        private Transform m_target;
        
        public void Initialize()
        {
            FetchComponents();
        }
        
        private void FetchComponents()
        {
            m_virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        }
        
        public void SetTarget(Transform target)
        {
            m_target = target;
            m_virtualCamera.Follow = m_target;
            m_virtualCamera.LookAt = m_target;
        }
    }
}
