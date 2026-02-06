using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player.Collector;
using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.Player
{
    public class PlayerEntity : MonoBehaviour, IDamageable
    {
        private CollisionBroadcaster2D  m_collisionBroadcaster2D;
        private MovementSystem m_movementSystem;
        private CombatSystem m_combatSystem;
        private WeaponCollectorSystem m_collectorSystem;

        private void Awake()
        {
            FetchComponents();
            Initialize();
        }

        private void OnEnable()
        {
            m_collectorSystem.Enable();
        }

        private void OnDisable()
        {
            m_collectorSystem.Disable();
        }

        protected void Initialize()
        {
            m_movementSystem.Initialize();
            m_combatSystem.Initialize();
            m_collectorSystem.OnWeaponCollected += m_combatSystem.AddWeapon;
        }

        protected void FetchComponents()
        {
            m_collisionBroadcaster2D = GetComponentInChildren<CollisionBroadcaster2D>();
            m_movementSystem = GetComponentInChildren<MovementSystem>();
            m_combatSystem = GetComponentInChildren<CombatSystem>();
            m_collectorSystem = new WeaponCollectorSystem(m_collisionBroadcaster2D);
        }

        public void OnDamage()
        {
            
        }
    }
}