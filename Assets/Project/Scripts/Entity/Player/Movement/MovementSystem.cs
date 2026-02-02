using Project.Scripts.InputManagement;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    public class MovementSystem : MonoBehaviour
    {
        [SerializeField] private MovementSettings m_movementSettings;
        private Rigidbody2D m_rb;

        private void Awake()
        {
            m_rb = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            PlayerInputData data = InputManager.Data;
            Vector2 targetDir = data.MovementInputAxisVec2;
            Vector2 currentVel =  m_rb.velocity;
            Vector2 desiredVel = targetDir * m_movementSettings.TranslationSpeed;
            Vector2 finalVel = Vector2.Lerp(currentVel, desiredVel, Time.deltaTime * (data.HasMovementInput ?  m_movementSettings.Acceleration :  m_movementSettings.Deceleration));
            m_rb.velocity = finalVel;
        }
    }
}