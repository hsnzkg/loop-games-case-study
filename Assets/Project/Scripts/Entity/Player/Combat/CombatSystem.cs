using Project.Scripts.Entity.Weapon;
using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private CombatSettings m_combatSettings;
        [SerializeField] private Transform m_root;

        private Transform m_weaponParent;
        private Collider2D m_playerCol;

        private WeaponEntity[] m_weapons;
        private int m_weaponCount;

        private ObjectPool<WeaponEntity> m_weaponPool;
        private bool m_isInitialized;

        private void Update()
        {
            if (!m_isInitialized)
            {
                return;
            }
            
            SnapWeaponParent();
            if (m_weaponCount <= 0) return;
            Cycle();
            for (int i = 0; i < m_weaponCount; i++)
            {
                ArrangeSmooth(m_weapons[i].transform, i);
            }
        }

        private void OnDrawGizmos()
        {
            if (!Application.isPlaying)
                return;

            Gizmos.color = Color.red;

            for (int i = 0; i < m_weaponCount; i++)
            {
                DrawWeaponOverlapGizmo(m_weapons[i]);
            }
        }

        private void DrawWeaponOverlapGizmo(WeaponEntity weapon)
        {
            Collider2D col = weapon.GetCollider();
            Transform t = weapon.transform;
            Vector2 center = t.TransformPoint(col.offset);
            Vector2 size = col.bounds.size;
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(center, 0.25f);
            Gizmos.DrawWireCube(center, size);
        }

        public void Initialize()
        {
            m_weaponParent = new GameObject($"Weapon_Parent_{GetInstanceID()}").transform;
            m_weaponParent.SetParent(m_root);
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

            m_isInitialized = true;
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
            Destroy(weapon.gameObject);
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
            return m_weaponPool.GetCountActive() < m_combatSettings.MaxWeaponCount;
        }
    }
}