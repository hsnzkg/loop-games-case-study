using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.Movement
{
    public class AIInputProvider : MonoBehaviour, IInputProvider
    {
        public Vector2 GetInput()
        {
            return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        }

        public bool GetHasInput()
        {
            return true;
        }
    }
}