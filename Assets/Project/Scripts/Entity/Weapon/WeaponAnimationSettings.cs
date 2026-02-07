using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Entity.Weapon
{
    [CreateAssetMenu(fileName = "WeaponAnimationSettings", menuName = "Project/WeaponAnimationSettings", order = 0)]
    public class WeaponAnimationSettings : ScriptableObject
    {
        public float Duration;
        public Ease Ease;
        public Vector3 StartScale;
        public Vector3 EndScale;
    }
}