using UnityEngine;

namespace Project.Scripts.Entity.Player.Combat
{
    public interface IDamageable
    {
        void OnDamage(float damage,Vector2 direction);
    }
}