using System;
using Project.Scripts.Entity.Weapon;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Player;
using Project.Scripts.Level;
using Project.Scripts.Pool;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using Project.Scripts.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Scripts.Spawning.Spawners
{
    public class WeaponCollectableSpawner : MonoBehaviour
    {
        [SerializeField] private WeaponCollectableSpawnerSettings m_weaponCollectorSettings;
        private GameObject m_parent;
        private ObjectPool<WeaponCollectableEntity> m_pool;
        private int m_activeCount;
        private float m_timer;
        private Vector2 m_minSpawn;
        private Vector2 m_maxSpawn;
        private WeaponCollectableEntity[] m_activeWeapons;
        private LevelManager  m_levelManager;
        private EventBind<EPlayerDead>  m_playerDeadEventBind;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            EventBus<EPlayerDead>.Register(m_playerDeadEventBind);
        }

        private void OnDisable()
        {
            EventBus<EPlayerDead>.Unregister(m_playerDeadEventBind);
        }

        private void Initialize()
        {
            m_playerDeadEventBind = new EventBind<EPlayerDead>(SpawnAroundRandom);
            m_levelManager = transform.parent.GetComponentInChildren<LevelManager>();
            m_activeWeapons = new WeaponCollectableEntity[m_weaponCollectorSettings.MaxWeaponsInWorld];
            m_parent = new GameObject("Weapon_Collectable_Parent");
            
            m_pool = new ObjectPool<WeaponCollectableEntity>(
                CreateInstance,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyInstance,
                collectionCheck: true,
                defaultCapacity: m_weaponCollectorSettings.PoolSize,
                maxSize: m_weaponCollectorSettings.MaxWeaponsInWorld
            );
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        private void Tick(float deltaTime)
        {
            m_timer += deltaTime;

            if (m_timer < m_weaponCollectorSettings.SpawnInterval)
                return;

            if (!CanSpawn())
                return;

            m_timer = 0f;
            SpawnRandom();
        }

        private bool CanSpawn()
        {
            return m_activeCount < m_weaponCollectorSettings.MaxWeaponsInWorld;
        }

        private void SpawnRandom()
        {
            Vector3 position = m_levelManager.GetRandomPointInArea(m_weaponCollectorSettings.InnerOffset);
            Spawn(position);
        }

        private void SpawnAroundRandom(EPlayerDead obj)
        {
            for (int i = 0; i < 5; i++)
            {
                if(!CanSpawn())break;
                Spawn(obj.Player.transform.position.ToVector2XY() + Random.insideUnitSphere.ToVector2XY());
            }
        }

        private void Spawn(Vector2 position)
        {
            WeaponCollectableEntity weapon = m_pool.Get();
            weapon.transform.SetPositionAndRotation(position, Quaternion.identity);
            weapon.OnSpawned();
        }

        #region Pool callbacks

        private WeaponCollectableEntity CreateInstance()
        {
            WeaponCollectableEntity instance = Instantiate(m_weaponCollectorSettings.WeaponPrefab, m_parent.transform);
            instance.Initialize(m_pool);
            instance.gameObject.SetActive(false);
            return instance;
        }

        private void OnGetFromPool(WeaponCollectableEntity instance)
        {
            instance.SetIsCollecting(false);
            instance.transform.localScale = m_weaponCollectorSettings.GetObjectDefaultSize();
            m_activeWeapons[m_activeCount] = instance;
            m_activeCount++;
            instance.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(WeaponCollectableEntity instance)
        {
            instance.transform.localScale = m_weaponCollectorSettings.GetObjectDefaultSize();
            m_activeWeapons[m_activeCount-1] = instance;
            m_activeCount--;
            instance.gameObject.SetActive(false);
        }

        private void OnDestroyInstance(WeaponCollectableEntity instance)
        {
            Destroy(instance.gameObject);
        }

        #endregion
        
        public WeaponCollectableEntity[] GetActiveWeapons()
        {
            return m_activeWeapons;
        }
    }
}