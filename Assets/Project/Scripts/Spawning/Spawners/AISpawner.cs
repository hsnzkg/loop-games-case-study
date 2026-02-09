using Project.Scripts.Entity.PlayerAI;
using Project.Scripts.Level;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using UnityEngine;

namespace Project.Scripts.Spawning.Spawners
{
    public class AISpawner : MonoBehaviour
    {
        [SerializeField] private int m_count;
        [SerializeField] private PlayerAIEntity m_playerAIPrefab;
        private PlayerAIEntity[] m_playerAIs;
        private bool m_spawned;

        public void Initialize()
        {
            m_playerAIs = new PlayerAIEntity[m_count];
            Spawn();
        }

        private void Spawn()
        {
            LevelManager levelManager = Storage<GameplayStorage>.GetInstance().LevelManager;
            for (int i = 0; i < m_count; i++)
            {
                PlayerAIEntity instance = Instantiate(m_playerAIPrefab,levelManager.GetRandomPointInArea(),Quaternion.identity);
                m_playerAIs[i] = instance;
            }
        }

        public PlayerAIEntity[] GetPlayers()
        {
            return m_playerAIs;
        }
    }
}