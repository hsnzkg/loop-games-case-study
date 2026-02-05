using Project.Scripts.Entity.Sword;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Combat
{
    [CreateAssetMenu(fileName = "CombatSettings", menuName = "Project/CombatSettings", order = 0)]
    public class CombatSettings : ScriptableObject
    {
        public LayerMask CombatLayer;
        public SwordEntity WeaponPrefab;
        public int StartWeaponCount;
        public int MaxWeaponCount;
        public float CycleSpeed;
        public float ArrangeTranslationSpeed;
        public float ArrangeRotationSpeed;
        public float CenterDistance;
    }
}