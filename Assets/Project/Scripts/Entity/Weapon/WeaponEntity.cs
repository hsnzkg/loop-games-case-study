using Project.Scripts.Collisions;
using Project.Scripts.Entity.Player;
using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Camera;
using Project.Scripts.Events.Scratch;
using Project.Scripts.Events.Weapon;
using Project.Scripts.Pool;
using Project.Scripts.Sound;
using UnityEngine;

namespace Project.Scripts.Entity.Weapon
{
    public class WeaponEntity : MonoBehaviour, IDamageable
    {
        [SerializeField] private WeaponAnimationSettings  m_animationSettings;
        [SerializeField] private Transform m_animationTransformTarget;
        [SerializeField] private WeaponAttributeSettings m_weaponAttributeSettings;
        private CollisionBroadcaster2D m_broadcaster2D;
        private IObjectPool<WeaponEntity> m_provider;
        private BoxCollider2D m_collider;
        private AnimationSystem m_animationSystem;
        private PlayerEntity m_playerEntity;

        private void Update()
        {
            EventBus<EScratch>.Raise(new EScratch(transform.position));
        }

        public void Initialize(PlayerEntity entity, IObjectPool<WeaponEntity> provider)
        {
            m_playerEntity = entity;
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
            m_animationSystem.OnDespawned();
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
                Vector2 dir = Vector2.ClampMagnitude(obj.transform.position - transform.position,1f);
                damageable.OnDamage(m_weaponAttributeSettings.Damage,dir);
            }

            if (damageable is WeaponEntity weaponEntity)
            {
                EventBus<EWeaponClashCollision>.Raise(new EWeaponClashCollision(m_collider,obj));
                if (m_playerEntity.CompareTag("Player") ||  weaponEntity.GetPlayerEntity().CompareTag("Player"))
                {
                    EventBus<EPlaySound>.Raise(new EPlaySound(SoundType.SwordHit,randomPitch:true));
                    EventBus<ECameraShake>.Raise(new ECameraShake());
                }
                Free();
            }
            else
            {
                if (m_playerEntity.CompareTag("Player"))
                {
                    EventBus<EPlaySound>.Raise(new EPlaySound(SoundType.PlayerHit));
                }
            }
        }

        public PlayerEntity GetPlayerEntity()
        {
            return m_playerEntity;
        }

        public void OnDamage(float damage,Vector2 direction)
        {
            Free();
        }
    }
}