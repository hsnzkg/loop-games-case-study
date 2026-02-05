using Project.Scripts.Entity.Sword;
using Project.Scripts.Level.Settings;
using UnityEngine;
using UnityEngine.Pool;

namespace Project.Scripts.Spawning.Spawners
{
    public class WeaponCollectableSpawner : MonoBehaviour
    {
        [Header("Spawn")]
        [SerializeField] private WeaponCollectableEntity m_weaponPrefab;
        [SerializeField] private int m_poolSize = 10;
        [SerializeField] private int m_maxWeaponsInWorld = 10;
        [SerializeField] private float m_spawnInterval = 2f;

        [Header("Level")]
        [SerializeField] private LevelSettings m_levelSettings;
        [SerializeField] private float m_innerOffset = 1.5f;

        private ObjectPool<WeaponCollectableEntity> m_pool;
        private int m_activeCount;

        private float m_timer;
        private Vector2 m_minSpawn;
        private Vector2 m_maxSpawn;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            CalculateSpawnArea();

            m_pool = new ObjectPool<WeaponCollectableEntity>(
                CreateInstance,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyInstance,
                collectionCheck: true,
                defaultCapacity: m_poolSize,
                maxSize: m_poolSize
            );
        }

        private void Update()
        {
            Tick(Time.deltaTime);
        }

        private void Tick(float deltaTime)
        {
            m_timer += deltaTime;

            if (m_timer < m_spawnInterval)
                return;

            if (!CanSpawn())
                return;

            m_timer = 0f;
            SpawnRandom();
        }

        public bool CanSpawn()
        {
            return m_activeCount < m_maxWeaponsInWorld;
        }

        private void SpawnRandom()
        {
            Vector3 position = GetRandomPointInArea();
            WeaponCollectableEntity weapon = m_pool.Get();

            weapon.transform.SetPositionAndRotation(position, Quaternion.identity);
            weapon.OnSpawned();
        }

        #region Pool callbacks

        private WeaponCollectableEntity CreateInstance()
        {
            WeaponCollectableEntity instance = Instantiate(m_weaponPrefab);
            instance.Initialize(m_pool);
            instance.gameObject.SetActive(false);
            return instance;
        }

        private void OnGetFromPool(WeaponCollectableEntity instance)
        {
            m_activeCount++;
            instance.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(WeaponCollectableEntity instance)
        {
            m_activeCount--;
            instance.gameObject.SetActive(false);
        }

        private void OnDestroyInstance(WeaponCollectableEntity instance)
        {
            Destroy(instance.gameObject);
        }

        #endregion

        private Vector3 GetRandomPointInArea()
        {
            float x = Random.Range(m_minSpawn.x, m_maxSpawn.x);
            float y = Random.Range(m_minSpawn.y, m_maxSpawn.y);
            return new Vector3(x, y, 0f);
        }

        private void CalculateSpawnArea()
        {
            float widthWorld  = m_levelSettings.Width  * m_levelSettings.TileSize;
            float heightWorld = m_levelSettings.Height * m_levelSettings.TileSize;

            Vector2 halfSize = new Vector2(widthWorld, heightWorld) * 0.5f;
            Vector2 center = m_levelSettings.Offset;

            float border =
                (m_levelSettings.BlankSpace * m_levelSettings.TileSize) +
                m_innerOffset;

            m_minSpawn = center - halfSize + Vector2.one * border;
            m_maxSpawn = center + halfSize - Vector2.one * border;
        }
    }
}
