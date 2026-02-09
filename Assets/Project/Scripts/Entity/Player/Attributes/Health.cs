using System;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Attributes
{
    [Serializable]
    public class Health
    {
        private float m_current;
        private readonly float m_max;
        public event Action<float> OnHealthChange;
        public event Action OnDeath;
        public bool IsDead;
        
        public Health(float health)
        {
            m_max = health;
            m_current = health;
            IsDead = false;
        }

        public void TakeDamage(float damage)
        {
            m_current -= damage;
            m_current = Mathf.Clamp(m_current, 0f, m_max);
            OnHealthChange?.Invoke(m_current);
            if (!(m_current <= 0f) || IsDead) return;
            IsDead = true;
            OnDeath?.Invoke();
        }

        public void Heal(float heal)
        {
            m_current += heal;
            m_current = Mathf.Clamp(m_current, 0f, m_max);
        }
    }
}