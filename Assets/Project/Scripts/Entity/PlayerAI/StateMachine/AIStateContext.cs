using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine
{
    public class AIStateContext
    {
        public readonly Transform EntityTransform;
        public readonly IInputProvider AIInputProvider;
        public readonly AISettings AISettings;

        public AIStateContext(AISettings aiSettings,IInputProvider inputProvider,Transform entityTransform)
        {
            AISettings = aiSettings;
            AIInputProvider = inputProvider;
            EntityTransform = entityTransform;
        }
    }
}