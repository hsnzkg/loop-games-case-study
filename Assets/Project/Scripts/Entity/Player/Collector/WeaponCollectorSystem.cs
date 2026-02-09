using System;
using DG.Tweening;
using Project.Scripts.Collisions;
using Project.Scripts.Entity.Weapon;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Collector
{
    public class WeaponCollectorSystem
    {
        private readonly WeaponCollectorSettings m_weaponCollectorSettings;
        private readonly Transform m_weaponSnapTarget;
        private readonly CollisionBroadcaster2D m_collisionBroadcaster2D;
        public event Action OnWeaponCollected;
        private Sequence m_collectSequence;
        private Tween m_scaleTween;

        public WeaponCollectorSystem(WeaponCollectorSettings weaponCollectorSettings,
            CollisionBroadcaster2D collisionBroadcaster2D,
            Transform weaponSnapTarget)
        {
            m_weaponCollectorSettings = weaponCollectorSettings;
            m_collisionBroadcaster2D = collisionBroadcaster2D;
            m_weaponSnapTarget = weaponSnapTarget;
        }

        public void Enable()
        {
            m_collisionBroadcaster2D.OnTriggerEnter2DEvent += OnTriggerEntered;
        }

        public void Disable()
        {
            m_scaleTween?.Kill();
            m_collectSequence?.Kill();
            m_collisionBroadcaster2D.OnTriggerEnter2DEvent -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D obj)
        {
            if (!obj.TryGetComponent(out ICollectable collectable) || collectable.GetIsCollecting()) return;

            collectable.SetIsCollecting(true);

            Transform item = obj.transform;
            Vector2 startPos = item.position;

            m_collectSequence = DOTween.Sequence();

            Tween snapTween = DOTween.To(
                () => 0f,
                t =>
                {
                    Vector2 targetPos = m_weaponSnapTarget.position;
                    item.position = Vector2.Lerp(startPos, targetPos, t);
                },
                1f,
                m_weaponCollectorSettings.Duration);
            snapTween.SetEase(Ease.InBack);


            m_scaleTween = obj.transform.DOScale(m_weaponCollectorSettings.SizeEndValue, m_weaponCollectorSettings.Duration);
            m_scaleTween.SetEase(m_weaponCollectorSettings.ScaleEase);
            m_collectSequence.Append(snapTween);
            m_collectSequence.Join(m_scaleTween);
            m_collectSequence.OnComplete(() => { OnSnap(collectable); });
            m_collectSequence.Play();
        }

        private void OnSnap(ICollectable collectable)
        {
            DOTween.Complete(m_collectSequence);
            collectable.Collect();
            OnWeaponCollected?.Invoke();
        }
    }
}