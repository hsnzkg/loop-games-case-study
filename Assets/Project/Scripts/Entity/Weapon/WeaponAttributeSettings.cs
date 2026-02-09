using UnityEngine;

namespace Project.Scripts.Entity.Weapon
{
    [CreateAssetMenu(fileName = "WeaponAttributeSettings", menuName = "Project/WeaponAttributeSettings", order = 0)]
    public class WeaponAttributeSettings : ScriptableObject
    {
        public float Damage;
    }
}