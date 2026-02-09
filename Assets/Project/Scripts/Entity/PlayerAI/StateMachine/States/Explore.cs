using Project.Scripts.Entity.PlayerAI.Movement;
using Project.Scripts.Level;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.States
{
    public class Explore : AIStateBase
    {
        private readonly LevelManager m_levelManager;
        private Vector2 m_currentDestinationPoint;
        public Explore(AIStateContext context) : base(context)
        {
            m_levelManager = Storage<GameplayStorage>.GetInstance().LevelManager;
        }

        protected override void OnEnter()
        {
            PickRandomDestinationPoint();
        }

        public override void Tick(float deltaTime)
        {
             CheckDestination();
        }

        private void CheckDestination()
        {
            float distance = Vector2.Distance(Context.EntityTransform.position.ToVector2XY(), m_currentDestinationPoint);
            if (distance <= Context.AISettings.DestinationReachThreshold)
            {
                PickRandomDestinationPoint();
            }
        }

        private void PickRandomDestinationPoint()
        {
            m_currentDestinationPoint = m_levelManager.GetRandomPointInArea();
            ((AIInputProvider)Context.AIInputProvider).SetPositionTarget(m_currentDestinationPoint);
        }
        
        public Vector2 GetCurrentDestinationPoint()
        {
            return m_currentDestinationPoint;
        }
    }
}