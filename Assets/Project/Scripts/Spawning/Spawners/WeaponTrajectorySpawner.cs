using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Weapon;
using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Spawning.Spawners
{
    public class WeaponTrajectorySpawner : MonoBehaviour
    {
        [SerializeField] private WeaponTrajectorySpawnerSettings m_trajectorySpawnerSettings;
        private GameObject m_parent;
        private ObjectPool<GameObject> m_pool;
        private int m_activeCount;
        private float m_timer;
        private Vector2 m_minSpawn;
        private Vector2 m_maxSpawn;
        private GameObject[] m_activeTrajectories;
        private EventBind<EWeaponClashCollision> m_clashCollision;

        private void Awake()
        {
            Initialize();
        }

        private void OnEnable()
        {
            EventBus<EWeaponClashCollision>.Register(m_clashCollision);
        }

        private void OnDisable()
        {
            EventBus<EWeaponClashCollision>.Unregister(m_clashCollision);
        }

        public void Initialize()
        {
            m_clashCollision = new EventBind<EWeaponClashCollision>(OnClash);
            m_activeTrajectories = new GameObject[m_trajectorySpawnerSettings.MaxWeaponsInWorld];
            m_parent = new GameObject("Weapon_Trajectory");
            
            m_pool = new ObjectPool<GameObject>(
                CreateInstance,
                OnGetFromPool,
                OnReleaseToPool,
                OnDestroyInstance,
                collectionCheck: true,
                defaultCapacity: m_trajectorySpawnerSettings.PoolSize,
                maxSize: m_trajectorySpawnerSettings.MaxWeaponsInWorld
            );
        }

        private void OnClash(EWeaponClashCollision obj)
        {
            if (!CanSpawn()) return;

            Collider2D from = obj.From;
            Collider2D to = obj.To;

            if (from == null || to == null) return;

            Vector2 dirFrom = (from.transform.position - to.transform.position).normalized;
            Vector2 dirTo = -dirFrom;

            GameObject t1 = m_pool.Get();
            GameObject t2 = m_pool.Get();

            t1.transform.SetPositionAndRotation(from.transform.position, from.transform.rotation);
            t2.transform.SetPositionAndRotation(to.transform.position, to.transform.rotation);

            LaunchTrajectory(t1.transform, dirFrom);
            LaunchTrajectory(t2.transform, dirTo);
        }
        
        private void LaunchTrajectory(Transform tr, Vector2 dir)
        {
            WeaponTrajectorySpawnerSettings s = m_trajectorySpawnerSettings;
            Vector3 start = tr.position;
            Vector3 end = start + (Vector3)(dir *  s.DistanceMultiplier);
            
            Sequence seq = DOTween.Sequence();
            
            TweenerCore<Vector3, Vector3, VectorOptions> moveTween = tr.DOMove(end, s.TweenDuration).SetEase(s.PositionEase);
            
            TweenerCore<Quaternion, Vector3, QuaternionOptions> rotateTween = tr.DORotate(
                    new Vector3(0, 0, s.SpinDegrees),
                    s.TweenDuration,
                    RotateMode.FastBeyond360)
                .SetEase(s.RotationEase);
            
            seq.Append(moveTween);
            seq.Join(rotateTween);
            seq.Play();
            seq.OnComplete(()=>
            {
                OnSequenceCompleted(tr);
            });
        }

        private void OnSequenceCompleted(Transform tr)
        {
            m_pool.Release(tr.gameObject);
        }

        private bool CanSpawn()
        {
            return m_activeCount < m_trajectorySpawnerSettings.MaxWeaponsInWorld;
        }

        private void Spawn(Vector2 position,Quaternion rotation)
        {
            if (!CanSpawn()) return;
            GameObject trajectory = m_pool.Get();
            trajectory.transform.SetPositionAndRotation(position, rotation);
        }

        #region Pool callbacks

        private GameObject CreateInstance()
        {
            GameObject instance = Instantiate(m_trajectorySpawnerSettings.Prefab, m_parent.transform);
            instance.gameObject.SetActive(false);
            return instance;
        }

        private void OnGetFromPool(GameObject instance)
        {
            instance.transform.localScale = m_trajectorySpawnerSettings.GetObjectDefaultSize();
            m_activeTrajectories[m_activeCount] = instance;
            m_activeCount++;
            instance.gameObject.SetActive(true);
        }

        private void OnReleaseToPool(GameObject instance)
        {
            instance.transform.localScale = m_trajectorySpawnerSettings.GetObjectDefaultSize();
            m_activeTrajectories[m_activeCount-1] = instance;
            m_activeCount--;
            instance.gameObject.SetActive(false);
        }

        private void OnDestroyInstance(GameObject instance)
        {
            Destroy(instance.gameObject);
        }

        #endregion
    }
}