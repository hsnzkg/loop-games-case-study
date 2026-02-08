using Project.Scripts.Camera;
using Project.Scripts.Level;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using UnityEngine;

namespace Project.Scripts
{
    public class GameManager
    {
        private PlayerSpawner m_playerSpawner;
        private WeaponCollectableSpawner m_weaponCollectableSpawner;
        private LevelManager m_levelManager;
        private CameraManager m_cameraManager;

        public void Initialize()
        {
            FetchComponents();
            
            m_cameraManager.Initialize();
            m_playerSpawner.Initialize();
            m_weaponCollectableSpawner.Initialize();
            m_levelManager.Generate();
        }

        private void FetchComponents()
        {
            m_weaponCollectableSpawner = Object.FindObjectOfType<WeaponCollectableSpawner>();
            m_playerSpawner = Object.FindObjectOfType<PlayerSpawner>();
            m_cameraManager = Object.FindObjectOfType<CameraManager>();
            m_levelManager = Object.FindObjectOfType<LevelManager>();

            Storage<GameplayStorage>.GetInstance().WeaponCollectableSpawner = m_weaponCollectableSpawner;
        }
    }
}