using System;
using Project.Scripts.Entity.Player;
using Project.Scripts.Entity.PlayerAI.StateMachine;
using Project.Scripts.Entity.PlayerAI.StateMachine.Predicates;
using Project.Scripts.Entity.PlayerAI.StateMachine.States;
using Project.Scripts.FiniteStateMachine.Runtime.RuntimeMode;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI
{
    public class PlayerAIEntity :  PlayerEntity
    {
        [SerializeField] private AISettings m_aiSettings;
        private FiniteStateMachine.Runtime.StateMachine m_stateMachine;
        private AIStateContext m_aiStateContext;
        private IdleToExplore m_idleToExplore;
        private NotPredicate m_exploreToCollect;
        private TickDispatcher m_tickDispatcher;
        
        private Explore m_explore;
        
        protected override void Initialize()
        {
            base.Initialize();
            m_tickDispatcher = new TickDispatcher();
            m_aiStateContext = new AIStateContext(m_aiSettings,InputProvider,transform);
            m_stateMachine = new FiniteStateMachine.Runtime.StateMachine(new AutoMode());

            m_explore = new Explore(m_aiStateContext);
            
            
            m_idleToExplore = new IdleToExplore(m_aiStateContext);
            m_exploreToCollect = new NotPredicate(m_idleToExplore);
            
            
            m_stateMachine.AddState(new Idle(m_aiStateContext));
            m_stateMachine.AddState(m_explore);
            m_stateMachine.AddState(new Collect(m_aiStateContext));
            
            m_stateMachine.AddTransition<Idle,Explore>(m_idleToExplore);
            //m_stateMachine.AddTransition<Explore,Collect>(m_exploreToCollect);
            
            m_stateMachine.SetDefaultState<Idle>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,m_aiSettings.VisionThreshold);
            Gizmos.DrawLine(transform.position,m_explore.GetCurrentDestinationPoint().ToVector3XY());
        }

        protected override void Update()
        {
            m_tickDispatcher.SetDelta(Time.deltaTime);
            m_stateMachine.Update();
            m_stateMachine.Dispatch(m_tickDispatcher);
        }
    }
}