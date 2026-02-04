using UnityEngine;

namespace Project.Scripts.InputManagement
{
    public class MobileInputReceiver : IInputReceiver
    {
        private readonly PlayerInputData m_data;
        private readonly DynamicJoystick m_dynamicJoystick;
        public MobileInputReceiver(PlayerInputData data)
        {
            m_data = data;
            m_dynamicJoystick = Object.FindObjectOfType<DynamicJoystick>();
            m_dynamicJoystick.OnInput += OnInput;
        }

        private void OnInput()
        {
            m_data.MovementInputAxisVec2Raw = m_dynamicJoystick.Direction;
        }
    }
}