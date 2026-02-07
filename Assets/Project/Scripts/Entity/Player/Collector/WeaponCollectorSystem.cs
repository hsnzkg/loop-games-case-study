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
            m_collisionBroadcaster2D.OnTriggerEnter2DEvent -= OnTriggerEntered;
        }

        private void OnTriggerEntered(Collider2D obj)
        {
            if (!obj.TryGetComponent(out ICollectable collectable) || collectable.GetIsCollecting()) return;

            collectable.SetIsCollecting(true);

            Transform item = obj.transform;
            Vector2 startPos = item.position;

            Sequence seq = DOTween.Sequence();

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


            Tween scaleTween = obj.transform.DOScale(m_weaponCollectorSettings.SizeEndValue,
                m_weaponCollectorSettings.Duration);
            scaleTween.SetEase(m_weaponCollectorSettings.ScaleEase);
            seq.Append(snapTween);
            seq.Join(scaleTween);
            seq.OnComplete(() => { OnSnap(collectable); });
            seq.Play();
        }

        private void OnSnap(ICollectable collectable)
        {
            collectable.Collect();
            OnWeaponCollected?.Invoke();
        }
    }
}