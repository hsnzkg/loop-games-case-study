using Project.Scripts.CameraManagement;
using Project.Scripts.Entity.Player;
using Project.Scripts.InputManagement;
using Project.Scripts.LevelManagement;
using Project.Scripts.Singleton;
using UnityEngine;

namespace Project.Scripts
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        private LevelManager m_levelManager;
        private CameraManager m_cameraManager;
        private PlayerEntity m_playerEntity;
        
        protected override void OnAwake()
        {
            Application.targetFrameRate = 60;
            FetchComponents();
            InputManager.Initialize();
            
            m_levelManager.Generate();
            
            m_cameraManager.Initialize();
            m_cameraManager.SetTarget(m_playerEntity.transform);
        }

        protected override void OnEnable()
        {
            InputManager.Enable();
        }

        protected override void OnDisable()
        {
            InputManager.Disable();
            m_levelManager.ClearLevel();
        }
        
        private void FetchComponents()
        {
            m_cameraManager = FindObjectOfType<CameraManager>();
            m_levelManager = FindObjectOfType<LevelManager>();
            m_playerEntity = FindObjectOfType<PlayerEntity>();
        }
    }
}