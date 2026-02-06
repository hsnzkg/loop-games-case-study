using Cinemachine;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Camera;
using UnityEngine;

namespace Project.Scripts.Camera
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CameraSettings m_cameraSettings;
        private CinemachineVirtualCamera m_virtualCamera;
        private Transform m_target;
        private EventBind<EChangeCameraTarget> m_changeCameraEb;
        
        public void Initialize()
        {
            m_changeCameraEb = new EventBind<EChangeCameraTarget>(SetTarget);
            EventBus<EChangeCameraTarget>.Register(m_changeCameraEb);
            FetchComponents();
        }
        
        private void FetchComponents()
        {
            m_virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
        }
        
        private void SetTarget(EChangeCameraTarget @event)
        {
            m_target = @event.Target;
            m_virtualCamera.Follow = m_target;
            m_virtualCamera.LookAt = m_target;
        }
    }
}
