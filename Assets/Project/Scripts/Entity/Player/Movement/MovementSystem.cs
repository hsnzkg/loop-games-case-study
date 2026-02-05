using Project.Scripts.Input;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private MovementSettings m_movementSettings;
        private Vector2 m_targetVelocity;
        private Rigidbody2D m_rb;

        public void Initialize()
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            PlayerInputData data = InputManager.GetData();
            Vector2 targetDir = data.GetMovementInputAxisVec2();
            Vector2 currentVel = m_rb.velocity;
            Vector2 desiredVel = targetDir * m_movementSettings.TranslationSpeed;
            float lerpSpeed = Time.fixedDeltaTime * (data.GetHasMovementInput()
                ? m_movementSettings.Acceleration
                : m_movementSettings.Deceleration);
            m_targetVelocity = Vector2.Lerp(currentVel, desiredVel, lerpSpeed);
            m_rb.velocity = m_targetVelocity;
        }
    }
}