using Project.Scripts.Entity.Weapon;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Combat
{
    [CreateAssetMenu(fileName = "CombatSettings", menuName = "Project/CombatSettings", order = 0)]
    public class CombatSettings : ScriptableObject
    {
        public WeaponEntity WeaponPrefab;
        public int StartWeaponCount;
        public int MaxWeaponCount;
        public float CycleSpeed;
        public float ArrangeTranslationSpeed;
        public float ArrangeRotationSpeed;
        public float CenterDistance;
    }
}