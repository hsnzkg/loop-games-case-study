using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.Entity.Sword;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Collector
{
    public class WeaponCollectorSystem : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask;
        private CombatSystem m_combatSystem;
        private CollisionBroadcaster2D m_collisionBroadcaster2D;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_combatSystem = GetComponent<CombatSystem>();
            m_collisionBroadcaster2D = GetComponent<CollisionBroadcaster2D>();
            m_collisionBroadcaster2D.OnTriggerEnter2DEvent += OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D obj)
        {
            if (!m_layerMask.Contains(obj.gameObject.layer)) return;
            OnCollected(obj.GetComponent<ICollectable>());
        }

        private void OnCollected(ICollectable collectable)
        {
            collectable.Collect();
            m_combatSystem.AddWeapon();
        }
    }
}