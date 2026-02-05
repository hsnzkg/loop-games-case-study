using UnityEngine;
using UnityEngine.Pool;

namespace Project.Scripts.Entity.Sword
{
    public class WeaponEntity : MonoBehaviour
    {
        private Pool.IObjectPool<WeaponEntity> m_provider;
        private Collider2D m_collider;
        
        private void FetchComponents()
        {
            m_collider = GetComponent<Collider2D>();
        }

        public void Initialize(Pool.IObjectPool<WeaponEntity> provider)
        {
            m_provider = provider;
            FetchComponents();
        }

        public Collider2D GetCollider()
        {
            return m_collider;
        }

        public void OnSpawned()
        {
        }

        public void Free()
        {
            m_provider.Release(this);
        }
    }
}