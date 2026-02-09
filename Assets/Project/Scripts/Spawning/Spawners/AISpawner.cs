using Project.Scripts.Entity.PlayerAI;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Player;
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
        private LevelManager m_levelManager;
        private PlayerAIEntity[] m_playerAIs;
        private bool m_spawned;
        private EventBind<EPlayerDead> m_playerDeadBind;

        private void Awake()
        {
            m_levelManager = transform.parent.GetComponentInChildren<LevelManager>();
            m_playerDeadBind = new EventBind<EPlayerDead>(OnPlayerDead);
            m_playerAIs = new PlayerAIEntity[m_count];
            Spawn();
        }

        private void OnEnable()
        {
            EventBus<EPlayerDead>.Register(m_playerDeadBind);
        }

        private void OnDisable()
        {
            EventBus<EPlayerDead>.Unregister(m_playerDeadBind);
        }

        public void Initialize()
        {
            
        }

        private void OnPlayerDead(EPlayerDead obj)
        {
            if (obj.Player.CompareTag("Player"))
            {
                EndCardController.Instance.OpenEndCard();
            }
            else if (obj.Player.CompareTag("PlayerAI"))
            {
                bool allDead = true;
                foreach (PlayerAIEntity ai in m_playerAIs)
                {
                    if (ai.GetIsDead()) continue;
                    allDead = false;
                    break;
                }

                if (allDead)
                {
                    EndCardController.Instance.OpenEndCard();
                }
            }
        }

        private void Spawn()
        {
            for (int i = 0; i < m_count; i++)
            {
                Vector3 position = m_levelManager.GetRandomPointInArea();
                PlayerAIEntity instance = Instantiate(m_playerAIPrefab,position,Quaternion.identity);
                m_playerAIs[i] = instance;
            }
        }

        public PlayerAIEntity[] GetPlayers()
        {
            return m_playerAIs;
        }
    }
}