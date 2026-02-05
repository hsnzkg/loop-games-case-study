using System;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.CollisionManagement
{
    public class CollisionBroadcaster2D : MonoBehaviour
    {
        [SerializeField] private LayerMask m_layerMask;

        public Action<Collision2D> OnCollisionEnter2DEvent;
        public Action<Collision2D> OnCollisionStay2DEvent;
        public Action<Collision2D> OnCollisionExit2DEvent;

        public Action<Collider2D> OnTriggerEnter2DEvent;
        public Action<Collider2D> OnTriggerStay2DEvent;
        public Action<Collider2D> OnTriggerExit2DEvent;


        private void OnCollisionEnter2D(Collision2D other)
        {
            if (m_layerMask.Contains(other.gameObject.layer))
            {
                OnCollisionEnter2DEvent?.Invoke(other);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (m_layerMask.Contains(other.gameObject.layer))
            {
                OnCollisionStay2DEvent?.Invoke(other);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (m_layerMask.Contains(other.gameObject.layer))
            {
                OnCollisionExit2DEvent?.Invoke(other);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (m_layerMask.Contains(other.gameObject.layer))
            {
                OnTriggerEnter2DEvent?.Invoke(other);
            }
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (m_layerMask.Contains(other.gameObject.layer))
            {
                OnTriggerStay2DEvent?.Invoke(other);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (m_layerMask.Contains(other.gameObject.layer))
            {
                OnTriggerExit2DEvent?.Invoke(other);
            }
        }
    }
}