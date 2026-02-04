using System;
using Project.Scripts.CollisionManagement;
using UnityEngine;

namespace Project.Scripts.Entity.Sword
{
    public class SwordEntity : MonoBehaviour
    {
        private CollisionBroadcaster2D m_collisionBroadcaster2D;
        private Collider2D m_collider;
        public event Action<SwordEntity> OnCollision;

        private void Awake()
        {
            FetchComponents();
            Initialize();
        }

        private void FetchComponents()
        {
            m_collider = GetComponent<Collider2D>();
            m_collisionBroadcaster2D = GetComponent<CollisionBroadcaster2D>();
        }

        private void Initialize()
        {
            m_collisionBroadcaster2D.OnCollisionEnter2DEvent += OnCollisionEntered;
        }

        public Collider2D GetCollider()
        {
            return m_collider;
        }

        private void OnCollisionEntered(Collision2D collision2D)
        {
            OnCollision?.Invoke(this);
        }
    }
}