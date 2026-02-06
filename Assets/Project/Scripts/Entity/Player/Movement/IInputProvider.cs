using UnityEngine;

namespace Project.Scripts.Entity.Player.Movement
{
    public interface IInputProvider
    {
        Vector2 GetInput();
        bool GetHasInput();
    }
}