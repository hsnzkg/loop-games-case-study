using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.MonoBehaviour;
using Project.Scripts.Events.Scratch;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    public class MovementSystem
    {
        private readonly MovementSettings m_movementSettings;
        private readonly IInputProvider m_inputProvider;
        private readonly Rigidbody2D m_rb;
        private Vector2 m_externalVelocity;
        private Vector2 m_targetVelocity;
        private EventBind<EUpdate>  m_updateBind;
        private EventBind<EFixedUpdate>  m_fixedUpdateBind;

        public MovementSystem(MovementSettings movementSettings,Rigidbody2D rb,IInputProvider inputProvider)
        {
            m_movementSettings = movementSettings;
            m_rb = rb;
            m_inputProvider = inputProvider;
        }

        public void Initialize()
        {
            m_updateBind = new EventBind<EUpdate>(Update);
            m_fixedUpdateBind = new EventBind<EFixedUpdate>(FixedUpdate);
        }

        public void Enable()
        {
            RegisterEvents();
        }

        public void Disable()
        {
            m_rb.velocity = Vector2.zero;
            m_rb.angularVelocity = 0;
            UnregisterEvents();
        }

        private void RegisterEvents()
        {
            EventBus<EFixedUpdate>.Register(m_fixedUpdateBind);
            EventBus<EUpdate>.Register(m_updateBind);
        }

        private void UnregisterEvents()
        {
            EventBus<EFixedUpdate>.Unregister(m_fixedUpdateBind);
            EventBus<EUpdate>.Unregister(m_updateBind);
        }

        private void Update()
        {
            EventBus<EScratch>.Raise(new EScratch(m_rb.transform.position));
        }

        private void FixedUpdate()
        {
            HandleMovement();
        }

        private void HandleMovement()
        {
            Vector2 targetDir = m_inputProvider.GetInput();
            Vector2 desiredVel = targetDir * m_movementSettings.TranslationSpeed;

            float accel = m_inputProvider.GetHasInput() ? m_movementSettings.Acceleration : m_movementSettings.Deceleration;

            float kMove = 1f - Mathf.Exp(-accel * Time.fixedDeltaTime);
            float kForce = 1f - Mathf.Exp(-m_movementSettings.ForceDampSpeed * Time.fixedDeltaTime);

            Vector2 baseVel = Vector2.Lerp(m_rb.velocity, desiredVel, kMove);

            m_rb.velocity = baseVel + m_externalVelocity;
            m_externalVelocity = Vector2.Lerp(m_externalVelocity, Vector2.zero, kForce);
        }

        public void AddForce(Vector2 force)
        {
            m_externalVelocity += force;
            if (m_externalVelocity.magnitude > m_movementSettings.MaxForce)
            {
                m_externalVelocity = m_externalVelocity.normalized * m_movementSettings.MaxForce;
            }
        }
    }
}