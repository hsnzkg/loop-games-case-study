using Project.Scripts.Entity.Weapon;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.MonoBehaviour;
using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Combat
{
    public class CombatSystem
    {
        private readonly CombatSettings m_combatSettings;
        private readonly Transform m_root;
        private Transform m_weaponParent;
        private readonly Collider2D m_playerCol;
        private WeaponEntity[] m_weapons;
        private ObjectPool<WeaponEntity> m_weaponPool;
        private int m_weaponCount;
        
        private readonly EventBind<EUpdate> m_updateBind;
        private PlayerEntity m_playerEntity;
        public CombatSystem(PlayerEntity entity, CombatSettings combatSettings, Transform root,Collider2D playerCol)
        {
            m_playerEntity = entity;
            m_combatSettings = combatSettings;
            m_root = root;
            m_playerCol = playerCol;
            m_updateBind = new EventBind<EUpdate>(Update);
        }

        public void Enable()
        {
            RegisterEvents();
        }

        public void Disable()
        {
            if (m_weaponCount > 0)
            {
                DestroyWeapons();
            }
            UnregisterEvents();
        }

        private void DestroyWeapons()
        {
            for (int i = 0; i < m_weaponCount; i++)
            {
                m_weaponPool.Release(m_weapons[i]);
            }
        }

        private void RegisterEvents()
        {
            EventBus<EUpdate>.Register(m_updateBind);
        }

        private void UnregisterEvents()
        {
            EventBus<EUpdate>.Unregister(m_updateBind);            
        }
        private void Update()
        {
            SnapWeaponParent();
            if (m_weaponCount <= 0) return;
            Cycle();
            for (int i = 0; i < m_weaponCount; i++)
            {
                ArrangeSmooth(m_weapons[i].transform, i);
            }
        }

        public void Initialize()
        {
            m_weaponParent = new GameObject($"Weapon_Parent_{m_root.GetInstanceID()}").transform;
            m_weaponParent.SetParent(m_root);
            m_weapons = new WeaponEntity[m_combatSettings.MaxWeaponCount];

            m_weaponPool = new ObjectPool<WeaponEntity>(
                CreateWeapon,
                OnGetWeapon,
                OnReleaseWeapon,
                OnDestroyWeapon,
                collectionCheck: false,
                defaultCapacity: m_combatSettings.StartWeaponCount,
                maxSize: m_combatSettings.MaxWeaponCount
            );

            for (int i = 0; i < m_combatSettings.StartWeaponCount; i++)
            {
                AddWeapon();
            }
        }

        #region Pool

        private WeaponEntity CreateWeapon()
        {
            WeaponEntity weapon = Object.Instantiate(m_combatSettings.WeaponPrefab, m_weaponParent);
            weapon.Initialize(m_playerEntity,m_weaponPool);
            return weapon;
        }

        private void OnGetWeapon(WeaponEntity weapon)
        {
            weapon.gameObject.SetActive(true);
            weapon.OnSpawned();
            SetCollisionLayers(weapon);
        }

        private void OnReleaseWeapon(WeaponEntity weapon)
        {
            int index = System.Array.IndexOf(m_weapons, weapon);
            if (index >= 0)
            {
                int last = m_weaponCount - 1;
                m_weapons[index] = m_weapons[last];
                m_weapons[last] = null;
                m_weaponCount--;
            }
            
            weapon.OnDespawned();
            weapon.gameObject.SetActive(false);
        }

        private void OnDestroyWeapon(WeaponEntity weapon)
        {
            weapon.OnDestroyed();
            Object.Destroy(weapon.gameObject);
        }

        #endregion

        public void AddWeapon()
        {
            if (m_weaponCount >= m_combatSettings.MaxWeaponCount)
            {
                return;
            }
            WeaponEntity weapon = m_weaponPool.Get();
            int index = m_weaponCount;
            m_weapons[index] = weapon;
            m_weaponCount++;

            ArrangeInstant(weapon.transform, index);
        }

        private void SetCollisionLayers(WeaponEntity weapon)
        {
            Physics2D.IgnoreCollision(m_playerCol, weapon.GetCollider());
        }

        private void GetDesiredTransform(int index, out Quaternion rot, out Vector3 pos)
        {
            float angleStep = 360f / m_weaponCount;
            float angle = index * angleStep;

            rot = Quaternion.Euler(0f, 0f, angle);
            pos = rot * Vector3.right * m_combatSettings.CenterDistance;
        }

        private void SnapWeaponParent()
        {
            m_weaponParent.position = m_root.position + m_combatSettings.CenterOffset;
        }

        private void ArrangeInstant(Transform t, int index)
        {
            GetDesiredTransform(index, out Quaternion rot, out Vector3 pos);
            t.localPosition = pos;
            t.localRotation = rot;
        }

        private void ArrangeSmooth(Transform t, int index)
        {
            GetDesiredTransform(index, out Quaternion rot, out Vector3 pos);

            float kPos = 1f - Mathf.Exp(-m_combatSettings.ArrangeTranslationSpeed * Time.deltaTime);
            float kRot = 1f - Mathf.Exp(-m_combatSettings.ArrangeRotationSpeed * Time.deltaTime);

            t.localPosition = Vector3.Lerp(t.localPosition, pos, kPos);
            t.localRotation = Quaternion.Slerp(t.localRotation, rot, kRot);
        }

        private void Cycle()
        {
            m_weaponParent.Rotate(Vector3.forward * (-m_combatSettings.CycleSpeed * Time.deltaTime));
        }

        public int GetWeaponCount()
        {
            return m_weaponCount;
        }
    }
}