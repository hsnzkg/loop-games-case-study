using System;
using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player.Collector;
using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.Player
{
    public class PlayerEntity : MonoBehaviour, IDamageable
    {
        [SerializeField] private MovementSettings m_movementSettings;
        [SerializeField] private WeaponCollectorSettings m_weaponCollectorSettings;
        [SerializeField] private CombatSettings m_combatSettings;
        [SerializeField] private Collider2D m_playerCollider;

        protected IInputProvider InputProvider;
        
        private CollisionBroadcaster2D  m_collisionBroadcaster2D;
        private MovementSystem m_movementSystem;
        private CombatSystem m_combatSystem;
        private WeaponCollectorSystem m_collectorSystem;

        protected virtual void Awake()
        {
            FetchComponents();
            Initialize();
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void Initialize()
        {
            m_movementSystem.Initialize();
            m_combatSystem.Initialize();
            m_collectorSystem.OnWeaponCollected += m_combatSystem.AddWeapon;
        }

        protected void FetchComponents()
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            InputProvider = GetComponent<IInputProvider>();
            
            m_collisionBroadcaster2D = GetComponentInChildren<CollisionBroadcaster2D>();
            m_movementSystem = new MovementSystem(m_movementSettings,rb,InputProvider);
            m_combatSystem = new CombatSystem(m_combatSettings,transform,m_playerCollider);
            m_collectorSystem = new WeaponCollectorSystem(m_weaponCollectorSettings,m_collisionBroadcaster2D,transform);
        }

        private void OnEnable()
        {
            m_collectorSystem.Enable();
            m_combatSystem.Enable();
            m_movementSystem.Enable();
        }

        private void OnDisable()
        {
            m_collectorSystem.Disable();
            m_combatSystem.Disable();
            m_movementSystem.Disable();
        }

        public void OnDamage()
        {
            
        }
    }
}