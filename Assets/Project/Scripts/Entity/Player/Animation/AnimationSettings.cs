using UnityEngine;

namespace Project.Scripts.Entity.Player.Animation
{
    [CreateAssetMenu(fileName = "AnimationSettings", menuName = "Project/AnimationSettings", order = 0)]
    public class AnimationSettings : ScriptableObject
    {
        [Header("Body Settings")] 
        public Texture2D BodyTexture;
        public Vector3 BodyWobbleDir;
        public float BodyWobbleAmount;
        public float BodyWobbleIdleSpeed;
        public float BodyWobbleWalkSpeed;
        public float SquashAmount;
        
        [Header("Bottom Settings")] 
        public Texture2D FootLTexture;
        public Texture2D FootRTexture;
        public Vector3 FootWobbleDir;
        public float FootWobbleAmount;
        public float FootWobbleIdleSpeed;
        public float FootWobbleWalkSpeed;
    }
}