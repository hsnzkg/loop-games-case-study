using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.InputManagement
{
    public class PlayerInputData
    {
        private Vector2 m_movementInputAxisVec2Raw;

        private Vector2 m_movementInputAxisVec2;

        private Vector3 m_movementInputAxisVec3Raw;

        private Vector3 m_movementInputAxisVec3;

        private bool m_hasMovementInput = false;

        public Vector2 GetMovementInputAxisVec2Raw()
        {
            return m_movementInputAxisVec2Raw;
        }

        public void SetMovementInputAxisVec2Raw(Vector2 value)
        {
            m_hasMovementInput = value.sqrMagnitude > Mathf.Epsilon;
            m_movementInputAxisVec2Raw = value;
            m_movementInputAxisVec3Raw = value.ToVector3XZ();
            m_movementInputAxisVec2 = value.ClampByUnit();
            m_movementInputAxisVec3 = value.ToVector3XZ().ClampByUnit();
        }

        public Vector2 GetMovementInputAxisVec2()
        {
            return m_movementInputAxisVec2;
        }

        public Vector3 GetMovementInputAxisVec3()
        {
            return m_movementInputAxisVec3;
        }

        public bool GetHasMovementInput()
        {
            return m_hasMovementInput;
        }

        public bool IsSprintPressing;

        public bool IsJumpPressedThisFrame;

        public void Reset()
        {
            m_movementInputAxisVec2Raw = Vector2.zero;
            m_movementInputAxisVec2 = Vector2.zero;
            m_movementInputAxisVec3Raw = Vector3.zero;
            m_movementInputAxisVec3 = Vector3.zero;
            m_hasMovementInput = false;
            IsSprintPressing = false;
            IsJumpPressedThisFrame = false;
        }

        public void Dispose()
        {
            Reset();
        }
    }
}