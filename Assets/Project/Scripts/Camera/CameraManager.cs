using System.Collections;
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
        private CinemachineImpulseSource m_impulseSource;

        private EventBind<EChangeCameraTarget> m_changeCameraEb;
        private EventBind<ECameraShake> m_cameraShakeEb;
        private bool m_isShaking;
        public void Initialize()
        {
            m_changeCameraEb = new EventBind<EChangeCameraTarget>(SetTarget);
            m_cameraShakeEb = new EventBind<ECameraShake>(OnCameraShake);

            EventBus<EChangeCameraTarget>.Register(m_changeCameraEb);
            EventBus<ECameraShake>.Register(m_cameraShakeEb);

            FetchComponents();
        }
        
        private void FetchComponents()
        {
            m_virtualCamera = GetComponentInChildren<CinemachineVirtualCamera>();
            m_impulseSource = GetComponent<CinemachineImpulseSource>();
        }
        

        private void OnCameraShake(ECameraShake e)
        {
            if (m_isShaking) return;

            ShakeRoutine(e);
        }

        private void ShakeRoutine(ECameraShake e)
        {
            m_isShaking = true;
            m_impulseSource.GenerateImpulse();
            m_isShaking = false;
        }
        private void SetTarget(EChangeCameraTarget @event)
        {
            m_target = @event.Target;
            m_virtualCamera.Follow = m_target;
            m_virtualCamera.LookAt = m_target;
        }
    }
}
