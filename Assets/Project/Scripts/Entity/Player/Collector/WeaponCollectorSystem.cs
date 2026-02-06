using System;
using Project.Scripts.Collisions;
using Project.Scripts.Entity.Weapon;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Collector
{
    public class WeaponCollectorSystem
    {
        private readonly CollisionBroadcaster2D m_collisionBroadcaster2D;
        public event Action OnWeaponCollected;
        
        public WeaponCollectorSystem(CollisionBroadcaster2D collisionBroadcaster2D)
        {
            m_collisionBroadcaster2D = collisionBroadcaster2D;
        }
        
        public void Enable()
        {
            m_collisionBroadcaster2D.OnTriggerEnter2DEvent += OnTriggerEntered;
        }

        public void Disable()
        {
            m_collisionBroadcaster2D.OnTriggerEnter2DEvent -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D obj)
        {
            if (obj.TryGetComponent(out ICollectable collectable) && obj.gameObject.activeSelf)
            {
                Collect(collectable);
            }
        }

        private void Collect(ICollectable collectable)
        {
            collectable.Collect();
            OnWeaponCollected?.Invoke();
        }
    }
}