using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private MovementSettings m_movementSettings;
        private IInputProvider m_inputProvider;
        private Vector2 m_targetVelocity;
        private Rigidbody2D m_rb;
        private bool m_isInitialized;

        public void Initialize()
        {
            FetchComponents();
            m_isInitialized = true;
        }

        private void FetchComponents()
        {
            m_rb = GetComponent<Rigidbody2D>();
            m_inputProvider = GetComponent<IInputProvider>();
        }

        private void FixedUpdate()
        {
            if (!m_isInitialized)
            {
                return;
            }
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 targetDir = m_inputProvider.GetInput();
            Vector2 currentVel = m_rb.velocity;
            Vector2 desiredVel = targetDir * m_movementSettings.TranslationSpeed;
            float lerpSpeed = Time.fixedDeltaTime * (m_inputProvider.GetHasInput()
                ? m_movementSettings.Acceleration
                : m_movementSettings.Deceleration);
            m_targetVelocity = Vector2.Lerp(currentVel, desiredVel, lerpSpeed);
            m_rb.velocity = m_targetVelocity;
        }
    }
}