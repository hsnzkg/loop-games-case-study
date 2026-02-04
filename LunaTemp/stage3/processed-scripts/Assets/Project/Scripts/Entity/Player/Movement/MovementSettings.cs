using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    [CreateAssetMenu(fileName = "MovementSettings", menuName = "Project/MovementSettings", order = 0)]
    public class MovementSettings : ScriptableObject
    {
        public float TranslationSpeed;
        public float Acceleration;
        public float Deceleration;
    }
}