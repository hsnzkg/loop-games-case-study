using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Entity.Weapon
{
    public class AnimationSystem
    {
        private readonly WeaponAnimationSettings m_animationSettings;
        private Tween m_scaleTween;
        private readonly Transform m_visualTransform;

        public AnimationSystem(WeaponAnimationSettings animationSettings, Transform visualTransform)
        {
            m_animationSettings = animationSettings;
            m_visualTransform = visualTransform;
        }

        public void OnSpawn()
        {
            m_scaleTween?.Kill();
            m_visualTransform.localScale = m_animationSettings.StartScale;
            m_scaleTween = m_visualTransform.DOScale(m_animationSettings.EndScale, m_animationSettings.Duration);
            m_scaleTween.SetEase(m_animationSettings.Ease);
            m_scaleTween.Play();
        }
    }
}