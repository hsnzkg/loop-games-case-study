using UnityEngine;

namespace Project.Scripts.InputManagement
{
    public class MobileInputReceiver : IInputReceiver
    {
        private readonly PlayerInputData m_Data;
        private DynamicJoystick m_dynamicJoystick;
        public MobileInputReceiver(PlayerInputData data)
        {
            m_Data = data;
            m_dynamicJoystick = Object.FindObjectOfType<DynamicJoystick>();
            m_dynamicJoystick.OnInput += OnInput;
        }

        private void OnInput()
        {
            m_Data.MovementInputAxisVec2Raw = m_dynamicJoystick.Direction;
        }
    }
}