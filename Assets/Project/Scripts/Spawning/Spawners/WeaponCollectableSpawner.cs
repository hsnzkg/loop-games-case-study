using Project.Scripts.Entity.Weapon;
using Project.Scripts.Pool;
using UnityEngine;

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

        public void Initialize()
        {
            m_activeWeapons = new WeaponCollectableEntity[m_weaponCollectorSettings.MaxWeaponsInWorld];
            
            m_parent = new GameObject("Weapon_Collectable_Parent");
            CalculateSpawnArea();

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

        public bool CanSpawn()
        {
            return m_activeCount < m_weaponCollectorSettings.MaxWeaponsInWorld;
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
            instance.SetIsCollecting(false);
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

        private Vector3 GetRandomPointInArea()
        {
            float x = Random.Range(m_minSpawn.x, m_maxSpawn.x);
            float y = Random.Range(m_minSpawn.y, m_maxSpawn.y);
            return new Vector3(x, y, 0f);
        }

        private void CalculateSpawnArea()
        {
            float widthWorld = m_weaponCollectorSettings.LevelSettings.Width *
                               m_weaponCollectorSettings.LevelSettings.TileSize;
            float heightWorld = m_weaponCollectorSettings.LevelSettings.Height *
                                m_weaponCollectorSettings.LevelSettings.TileSize;

            Vector2 halfSize = new Vector2(widthWorld, heightWorld) * 0.5f;
            Vector2 center = m_weaponCollectorSettings.LevelSettings.Offset;

            float border =
                m_weaponCollectorSettings.LevelSettings.BlankSpace * m_weaponCollectorSettings.LevelSettings.TileSize +
                m_weaponCollectorSettings.InnerOffset;

            m_minSpawn = center - halfSize + Vector2.one * border;
            m_maxSpawn = center + halfSize - Vector2.one * border;
        }
    }
}