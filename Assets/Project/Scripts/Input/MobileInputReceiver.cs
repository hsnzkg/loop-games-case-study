using Project.ThirdParty.Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Project.Scripts.Input
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
            m_data.SetMovementInputAxisVec2Raw(m_dynamicJoystick.GetDirection());
        }
    }
}