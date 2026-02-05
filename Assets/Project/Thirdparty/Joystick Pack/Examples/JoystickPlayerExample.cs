using Project.Thirdparty.Joystick_Pack.Scripts.Joysticks;
using UnityEngine;

namespace Project.Thirdparty.Joystick_Pack.Examples
{
    public class JoystickPlayerExample : MonoBehaviour
    {
        public float speed;
        public VariableJoystick variableJoystick;
        public Rigidbody rb;

        public void FixedUpdate()
        {
            Vector3 direction = Vector3.forward * variableJoystick.GetVertical() + Vector3.right * variableJoystick.GetHorizontal();
            rb.AddForce(direction * speed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }
}