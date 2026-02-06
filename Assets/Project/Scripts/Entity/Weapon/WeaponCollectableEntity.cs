using Project.Scripts.Pool;
using UnityEngine;

namespace Project.Scripts.Entity.Weapon
{
    public class WeaponCollectableEntity : MonoBehaviour, ICollectable
    {
        private IObjectPool<WeaponCollectableEntity> m_provider;
        
        public void Initialize(IObjectPool<WeaponCollectableEntity> provider)
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