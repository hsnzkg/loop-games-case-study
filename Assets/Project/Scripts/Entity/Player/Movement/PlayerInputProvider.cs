using Project.Scripts.Input;
using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    public class PlayerInputProvider : MonoBehaviour, IInputProvider
    {
        public Vector2 GetInput()
        {
            return InputManager.GetData().GetMovementInputAxisVec2();
        }

        public bool GetHasInput()
        {
            return InputManager.GetData().GetHasMovementInput();
        }
    }
}