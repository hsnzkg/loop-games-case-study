using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI
{
    [CreateAssetMenu(fileName = "AISettings", menuName = "Project/AISettings", order = 0)]
    public class AISettings : ScriptableObject
    {
        public float VisionThreshold;
        public float DestinationReachThreshold;
    }
}