using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Collector
{
    [CreateAssetMenu(fileName = "WeaponCollectorSettings", menuName = "Project/WeaponCollectorSettings", order = 0)]
    public class WeaponCollectorSettings : ScriptableObject
    {
        public float Duration;
        public Ease PositionEase;
        public Ease ScaleEase;
        public Vector3 SizeEndValue;
    }
}