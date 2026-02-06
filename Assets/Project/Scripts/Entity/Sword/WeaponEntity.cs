using System;
using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Entity.Sword
{
    public class WeaponEntity : MonoBehaviour, IDamageable
    {
        private CollisionBroadcaster2D m_broadcaster2D;
        private IObjectPool<WeaponEntity> m_provider;
        private Collider2D m_collider;
        
        private void FetchComponents()
        {
            m_broadcaster2D = GetComponent<CollisionBroadcaster2D>();
            m_collider = GetComponent<Collider2D>();
        }

        public void Initialize(IObjectPool<WeaponEntity> provider)
        {
            m_provider = provider;
            FetchComponents();
        }

        public Collider2D GetCollider()
        {
            return m_collider;
        }

        public void OnSpawned()
        {
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
            if (obj.TryGetComponent(out IDamageable damageable))
            {
                damageable.OnDamage();
            }
        }

        public void OnDamage()
        {
            Free();
        }
    }
}