using System;
using Project.Scripts.Entity.Sword;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Combat
{
    public class CombatSystem : MonoBehaviour
    {
        [SerializeField] private CombatSettings m_combatSettings;
        [SerializeField] private Transform m_weaponParent;
        
        private Collider2D m_col;
        private SwordEntity[] m_weapons;
        private int m_weaponCount;

        private void Awake()
        {
            Initialize();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AddWeapon();
            }

            if (m_weaponCount <= 0) return;
            Cycle();
            for (int i = 0; i < m_weaponCount; i++)
            {   
                ArrangeSmooth(m_weapons[i].transform, i);
            }
        }

        private void Initialize()
        {
            m_col = GetComponent<Collider2D>();
            m_weapons = new SwordEntity[m_combatSettings.MaxWeaponCount];
            for (int i = 0; i < m_combatSettings.StartWeaponCount; i++)
            {
                AddWeapon();
            }
        }

        public void AddWeapon()
        {
            if (m_weaponCount >= m_combatSettings.MaxWeaponCount) return;
            SwordEntity weaponInstance = Instantiate(m_combatSettings.WeaponPrefab, m_weaponParent);
            weaponInstance.OnCollision += OnWeaponCollision;
            SetCollisionLayers(weaponInstance);
            int index = m_weaponCount;
            m_weapons[index] = weaponInstance;
            m_weaponCount++;
            ArrangeInstant(weaponInstance.transform, index);
        }

        private void SetCollisionLayers(SwordEntity obj)
        {
            Physics2D.IgnoreCollision(m_col, obj.GetCollider());
            for (int i = 0; i < m_weaponCount; i++)
            {
                Physics2D.IgnoreCollision(obj.GetCollider(), m_weapons[i].GetCollider());
            }
        }

        private void OnWeaponCollision(SwordEntity obj)
        {
            int index = Array.IndexOf(m_weapons, obj);
            if (index < 0) return;

            obj.OnCollision -= OnWeaponCollision;

            m_weaponCount--;
            m_weapons[index] = m_weapons[m_weaponCount];
            m_weapons[m_weaponCount] = null;

            Destroy(obj.gameObject);
        }

        private void GetDesiredTransform(
            int index,
            out Quaternion desiredRot,
            out Vector3 desiredPos)
        {
            float angleStep = 360f / m_weaponCount;
            float angle = index * angleStep;

            desiredRot = Quaternion.Euler(0f, 0f, angle);
            desiredPos = desiredRot * Vector3.right * m_combatSettings.CenterDistance;
        }

        private void ArrangeInstant(Transform t, int index)
        {
            GetDesiredTransform(index, out var desiredRot, out var desiredPos);

            t.localPosition = desiredPos;
            t.localRotation = desiredRot;
        }

        private void ArrangeSmooth(Transform t, int index)
        {
            GetDesiredTransform(index, out Quaternion desiredRot, out Vector3 desiredPos);

            float kPos = 1f - Mathf.Exp(-m_combatSettings.ArrangeTranslationSpeed * Time.deltaTime);

            float kRot = 1f - Mathf.Exp(-m_combatSettings.ArrangeRotationSpeed * Time.deltaTime);

            t.localPosition = Vector3.Lerp(t.localPosition, desiredPos, kPos);
            t.localRotation = Quaternion.Slerp(t.localRotation, desiredRot, kRot);
        }

        private void Cycle()
        {
            m_weaponParent.Rotate(Vector3.forward * (-m_combatSettings.CycleSpeed * Time.deltaTime));
        }
    }
}