using System;
using Project.Scripts.Entity.Player.Movement;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.Movement
{
    public class AIInputProvider : MonoBehaviour, IInputProvider
    {
        [SerializeField] private Transform m_transform;
        private Vector2 m_positionTarget;

        private void Awake()
        {
            m_positionTarget = m_transform.position;
        }

        public Vector2 GetInput()
        {
            return Vector2.ClampMagnitude(m_positionTarget -  m_transform.position.ToVector2XY(),1f);
        }

        public bool GetHasInput()
        {
            return true;
        }

        public void SetPositionTarget(Vector2 position)
        {
            m_positionTarget = position;
        }
        
        public Vector2 GetPositionTarget()
        {
            return m_positionTarget;
        }
    }
}