using System;
using Project.Scripts.Entity.Sword;
using UnityEngine;
using UnityEngine.Pool;

namespace Project.Scripts.Entity.Player.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private CombatSettings m_combatSettings;
        [SerializeField] private Transform m_root;
        [SerializeField] private int m_maxCollisionCount = 64;

        private Transform m_weaponParent;
        private Collider2D m_playerCol;

        private WeaponEntity[] m_weapons;
        private int m_weaponCount;

        private Collider2D[] m_collisionBuffer;
        private ContactFilter2D m_filter;

        private ObjectPool<WeaponEntity> m_weaponPool;

        private void Update()
        {
            SnapWeaponParent();

            if (m_weaponCount <= 0)
                return;

            Cycle();

            for (int i = 0; i < m_weaponCount; i++)
            {
                ArrangeSmooth(m_weapons[i].transform, i);
                CheckCollisionForWeapon(i);
            }
        }

        public void Initialize()
        {
            m_collisionBuffer = new Collider2D[m_maxCollisionCount];

            m_filter = new ContactFilter2D
            {
                useTriggers = true,
            };
            m_filter.SetLayerMask(m_combatSettings.CombatLayer);

            m_weaponParent = new GameObject($"Weapon_Parent_{GetInstanceID()}").transform;

            m_playerCol = GetComponent<Collider2D>();
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
            WeaponEntity weapon = Instantiate(m_combatSettings.WeaponPrefab, m_weaponParent);
            weapon.Initialize(m_weaponPool);
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
            weapon.gameObject.SetActive(false);
        }

        private void OnDestroyWeapon(WeaponEntity weapon)
        {
            Destroy(weapon.gameObject);
        }

        #endregion

        public void AddWeapon()
        {
            if (m_weaponCount >= m_combatSettings.MaxWeaponCount)
                return;

            WeaponEntity weapon = m_weaponPool.Get();

            int index = m_weaponCount;
            m_weapons[index] = weapon;
            m_weaponCount++;

            ArrangeInstant(weapon.transform, index);
        }

        private void CheckCollisionForWeapon(int index)
        {
            Array.Clear(m_collisionBuffer, 0, m_collisionBuffer.Length);
            WeaponEntity weapon = m_weapons[index];
            Collider2D col = weapon.GetCollider();

            int count = Physics2D.OverlapCollider(col, m_filter, m_collisionBuffer);
            if (count == 0) return;
            for (int i = 0; i < m_maxCollisionCount; i++)
            {
                Collider2D resultCol = m_collisionBuffer[i];
                
                if(resultCol == null)continue;
                if (IsOur(resultCol.transform)) continue;
                
                m_weaponCount--;
                m_weapons[index] = m_weapons[m_weaponCount];
                m_weapons[m_weaponCount] = null;
                m_weaponPool.Release(weapon);
                break;
            }
        }

        private bool IsOur(Transform t)
        {
            return t.IsChildOf(m_weaponParent);
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
            m_weaponParent.position = m_root.position;
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

        public bool CanSpawn()
        {
            return m_weaponPool.CountActive < m_combatSettings.MaxWeaponCount;
        }
    }
}
