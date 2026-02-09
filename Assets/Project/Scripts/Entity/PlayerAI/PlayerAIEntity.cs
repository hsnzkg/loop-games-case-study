using System;
using Project.Scripts.Entity.Player;
using Project.Scripts.Entity.PlayerAI.Movement;
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
        private NotPredicate m_exitFromEscape;
        private NotPredicate m_exitFromChase;
        private ToEscape m_escape;
        private ToChase m_chase;
        private TickDispatcher m_tickDispatcher;
        
        protected override void Initialize()
        {
            base.Initialize();
            m_tickDispatcher = new TickDispatcher();
            m_aiStateContext = new AIStateContext(this,m_aiSettings,InputProvider,transform);
            m_stateMachine = new FiniteStateMachine.Runtime.StateMachine(new AutoMode());
            
            m_idleToExplore = new IdleToExplore(m_aiStateContext);
            m_exploreToCollect = new NotPredicate(m_idleToExplore);
            
            m_escape = new ToEscape(m_aiStateContext);
            m_chase = new ToChase(m_aiStateContext);
            
            m_exitFromEscape = new NotPredicate(m_escape);
            m_exitFromChase = new NotPredicate(m_chase);
            
            m_stateMachine.AddState(new Idle(m_aiStateContext));
            m_stateMachine.AddState(new Explore(m_aiStateContext));
            m_stateMachine.AddState(new Collect(m_aiStateContext));
            m_stateMachine.AddState(new Escape(m_aiStateContext));
            m_stateMachine.AddState(new Chase(m_aiStateContext));
            
            m_stateMachine.AddTransition<Idle,Explore>(m_idleToExplore);
            m_stateMachine.AddTransition<Idle,Collect>(m_exploreToCollect);
            m_stateMachine.AddTransition<Idle,Escape>(m_escape);
            m_stateMachine.AddTransition<Idle,Chase>(m_chase);
            m_stateMachine.AddTransition<Explore,Idle>(new NotPredicate(m_idleToExplore));
            m_stateMachine.AddTransition<Explore,Collect>(m_exploreToCollect);
            m_stateMachine.AddTransition<Explore,Escape>(m_escape);
            m_stateMachine.AddTransition<Explore,Chase>(m_chase);
            m_stateMachine.AddTransition<Explore,Chase>(m_chase);
            m_stateMachine.AddTransition<Collect,Idle>(m_idleToExplore);
            m_stateMachine.AddTransition<Collect,Escape>(m_escape);
            m_stateMachine.AddTransition<Collect,Chase>(m_chase);
            m_stateMachine.AddTransition<Collect,Explore>(m_idleToExplore);
            m_stateMachine.AddTransition<Chase,Idle>(m_exitFromChase);
            m_stateMachine.AddTransition<Escape,Idle>(m_exitFromEscape);
            
            m_stateMachine.SetDefaultState<Idle>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,m_aiSettings.CollectableVisionThreshold);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,m_aiSettings.DestinationReachThreshold);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position,((AIInputProvider)InputProvider).GetPositionTarget().ToVector3XY());
            
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position,m_aiSettings.RunAwayVisionThreshold);
            
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(transform.position,m_aiSettings.ChaseVisionThreshold);
        }

        protected override void Update()
        {
            m_tickDispatcher.SetDelta(Time.deltaTime);
            m_stateMachine.Update();
            m_stateMachine.Dispatch(m_tickDispatcher);
        }
    }
}