using System;
using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Scratch;
using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Entity.Weapon
{
    public class WeaponEntity : MonoBehaviour, IDamageable
    {
        [SerializeField] private WeaponAnimationSettings  m_animationSettings;
        [SerializeField] private Transform m_animationTransformTarget;
        private CollisionBroadcaster2D m_broadcaster2D;
        private IObjectPool<WeaponEntity> m_provider;
        private BoxCollider2D m_collider;
        private AnimationSystem m_animationSystem;


        private void Update()
        {
            EventBus<EScratch>.Raise(new EScratch(transform.position));
        }

        public void Initialize(IObjectPool<WeaponEntity> provider)
        {
            m_animationSystem = new AnimationSystem(m_animationSettings,m_animationTransformTarget);
            m_provider = provider;
            FetchComponents();
        }

        private void FetchComponents()
        {
            m_broadcaster2D = GetComponent<CollisionBroadcaster2D>();
            m_collider = GetComponent<BoxCollider2D>();
        }

        public BoxCollider2D GetCollider()
        {
            return m_collider;
        }

        public void OnSpawned()
        {
            m_animationSystem.OnSpawn();
            m_broadcaster2D.OnTriggerEnter2DEvent += OnTriggerEvent;
        }

        public void OnDespawned()
        {
            m_broadcaster2D.OnTriggerEnter2DEvent -= OnTriggerEvent;
        }

        public void OnDestroyed()
        {
            m_broadcaster2D.OnTriggerEnter2DEvent -= OnTriggerEvent;
        }

        private void Free()
        {
            m_provider.Release(this);
        }

        private void OnTriggerEvent(Collider2D obj)
        {
            Control(obj);
        }

        private void Control(Collider2D obj)
        {
            if (obj.TryGetComponent(out WeaponEntity weaponEntity))
            {
                OnDamage();
            }
        }

        public void OnDamage()
        {
            Free();
        }
    }
}