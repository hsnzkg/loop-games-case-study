using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine
{
    public class AIStateContext
    {
        public readonly Transform EntityTransform;
        public readonly IInputProvider AIInputProvider;
        public readonly AISettings AISettings;
        public readonly PlayerAIEntity Entity;

        public AIStateContext(PlayerAIEntity entity, AISettings aiSettings,IInputProvider inputProvider,Transform entityTransform)
        {
            Entity = entity;
            AISettings = aiSettings;
            AIInputProvider = inputProvider;
            EntityTransform = entityTransform;
        }
    }
}