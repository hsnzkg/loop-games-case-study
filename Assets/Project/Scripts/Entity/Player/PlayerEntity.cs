using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player.Attributes;
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
        [SerializeField] private PlayerAttributeSettings m_playerAttributeSettings;
        private Health m_health;
        protected IInputProvider InputProvider;
        private CollisionBroadcaster2D  m_collisionBroadcaster2D;
        private MovementSystem m_movementSystem;
        private CombatSystem m_combatSystem;
        private WeaponCollectorSystem m_collectorSystem;
        private Rigidbody2D m_rb;

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
            m_health = new Health(m_playerAttributeSettings.Health);
            m_health.OnDeath += OnDeath;
            m_movementSystem.Initialize();
            m_combatSystem.Initialize();
            m_collectorSystem.OnWeaponCollected += m_combatSystem.AddWeapon;
        }

        protected void FetchComponents()
        {
            m_rb= GetComponent<Rigidbody2D>();
            InputProvider = GetComponent<IInputProvider>();
            
            m_collisionBroadcaster2D = GetComponentInChildren<CollisionBroadcaster2D>();
            m_movementSystem = new MovementSystem(m_movementSettings,m_rb,InputProvider);
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

        public virtual void OnDamage(float damage,Vector2 direction)
        {
            m_health.TakeDamage(damage);
            m_movementSystem.AddForce(direction * m_movementSettings.DamageForce);
        }

        private void OnDeath()
        {
            Debug.Log(gameObject.name + " has been death");
            m_collectorSystem.Disable();
            m_combatSystem.Disable();
            m_movementSystem.Disable();
            Destroy(gameObject);
        }
    }
}