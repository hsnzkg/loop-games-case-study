using Project.Scripts.CameraManagement;
using Project.Scripts.CameraManagement.Events;
using Project.Scripts.Entity.Player;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.LevelManagement;
using UnityEngine;

namespace Project.Scripts
{
    public class GameManager
    {
        private LevelManager m_levelManager;
        private CameraManager m_cameraManager;
        private PlayerEntity m_playerEntity;

        public void Initialize()
        {
            FetchComponents();
            
            m_levelManager.Generate();
            m_playerEntity.Initialize();
            m_cameraManager.Initialize();
            
            EventBus<EChangeCameraTarget>.Raise(new EChangeCameraTarget(m_playerEntity.transform));
        }

        private void FetchComponents()
        {
            m_cameraManager = Object.FindObjectOfType<CameraManager>();
            m_levelManager = Object.FindObjectOfType<LevelManager>();
            m_playerEntity = Object.FindObjectOfType<PlayerEntity>();
        }
    }
}