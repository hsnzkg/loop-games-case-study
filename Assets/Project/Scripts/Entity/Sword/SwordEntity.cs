using UnityEngine;

namespace Project.Scripts.Entity.Sword
{
    public class SwordEntity : MonoBehaviour
    {
        private Collider2D m_collider;
        
        private void FetchComponents()
        {
            m_collider = GetComponent<Collider2D>();
        }

        public void Initialize()
        {
            FetchComponents();
        }

        public Collider2D GetCollider()
        {
            return m_collider;
        }
    }
}