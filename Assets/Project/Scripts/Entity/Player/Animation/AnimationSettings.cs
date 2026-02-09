using DG.Tweening;
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
        
        [Header("Hurt")]
        public float HurtFadeIn = 0.1f;
        public float HurtFadeOut = 0.2f;
        public Ease HurtEase = Ease.OutQuad;
        
        [Header("Death")]
        public float DeathMoveDuration = 0.6f;
        public float DeathRotateDuration = 0.6f;
        public float DeathScaleDuration = 0.6f;

        public float DeathEndScale = 0.2f;
        public float DeathMoveDistance = 2.5f;
        public float DeathRandomRadius = 0.6f;

        public Ease DeathMoveEase = Ease.OutQuad;
        public Ease DeathRotateEase = Ease.OutQuad;
        public Ease DeathScaleEase = Ease.InQuad;
    }
}