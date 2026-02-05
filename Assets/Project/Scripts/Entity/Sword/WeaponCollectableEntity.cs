using UnityEngine;
using UnityEngine.Pool;

namespace Project.Scripts.Entity.Sword
{
    public class WeaponCollectableEntity : MonoBehaviour, ICollectable
    {
        private Pool.IObjectPool<WeaponCollectableEntity> m_provider;
        
        public void Initialize(Pool.IObjectPool<WeaponCollectableEntity> provider)
        {
            m_provider = provider;
        }
        
        public void OnSpawned()
        {
        }
        
        public void Collect()
        {
            m_provider.Release(this);
        }
    }
}