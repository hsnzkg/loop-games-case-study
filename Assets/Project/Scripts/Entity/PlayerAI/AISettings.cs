using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI
{
    [CreateAssetMenu(fileName = "AISettings", menuName = "Project/AISettings", order = 0)]
    public class AISettings : ScriptableObject
    {
        public float CollectableVisionThreshold;
        public float RunAwayVisionThreshold;
        public float ChaseVisionThreshold;
        public float DestinationReachThreshold;
        public float IsDangerHealthPercentageThreshold;
        public bool IsDangerWeaponAdvantageImportant;
        public float EscapeMoveLenght;
    }
}