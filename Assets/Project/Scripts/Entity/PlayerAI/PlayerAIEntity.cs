using System;
using Project.Scripts.Entity.Player;
using Project.Scripts.Entity.PlayerAI.Movement;
using Project.Scripts.Entity.PlayerAI.StateMachine;
using Project.Scripts.Entity.PlayerAI.StateMachine.Predicates;
using Project.Scripts.Entity.PlayerAI.StateMachine.States;
using Project.Scripts.FiniteStateMachine.Runtime.RuntimeMode;
using Project.Scripts.Level;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Utility;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Scripts.Entity.PlayerAI
{
    public class PlayerAIEntity :  PlayerEntity
    {
        [SerializeField] private AISettings m_aiSettings;
        private FiniteStateMachine.Runtime.StateMachine m_stateMachine;
        private AIStateContext m_aiStateContext;
        private ToExplore m_toExplore;
        private NotPredicate m_toCollect;
        private NotPredicate m_exitFromEscape;
        private NotPredicate m_exitFromChase;
        private ToEscape m_escape;
        private ToChase m_chase;
        private TickDispatcher m_tickDispatcher;
        private WeaponCollectableSpawner  m_collectableSpawner;
        private AISpawner m_aiSpawner;
        private LevelManager m_levelManager;
        private WeaponCollectableSpawner m_weaponCollectableSpawner;
        protected override void Initialize()
        {
            base.Initialize();
            m_collectableSpawner = FindObjectOfType<WeaponCollectableSpawner>();
            m_aiSpawner = FindObjectOfType<AISpawner>();
            m_levelManager = FindObjectOfType<LevelManager>();
            m_weaponCollectableSpawner = FindObjectOfType<WeaponCollectableSpawner>();
            
            m_tickDispatcher = new TickDispatcher();
            m_aiStateContext = new AIStateContext(this,m_aiSettings,InputProvider,transform);
            m_stateMachine = new FiniteStateMachine.Runtime.StateMachine(new AutoMode());
            
            m_toExplore = new ToExplore(m_aiStateContext,m_collectableSpawner);
            m_toCollect = new NotPredicate(m_toExplore);
            m_escape = new ToEscape(m_aiStateContext,m_aiSpawner);
            m_chase = new ToChase(m_aiStateContext,m_aiSpawner);
            
            m_exitFromEscape = new NotPredicate(m_escape);
            m_exitFromChase = new NotPredicate(m_chase);
            
            m_stateMachine.AddState(new Idle(m_aiStateContext));
            m_stateMachine.AddState(new Explore(m_aiStateContext,m_levelManager));
            m_stateMachine.AddState(new Collect(m_aiStateContext,m_weaponCollectableSpawner));
            m_stateMachine.AddState(new Escape(m_aiStateContext,m_aiSpawner,m_levelManager));
            m_stateMachine.AddState(new Chase(m_aiStateContext,m_aiSpawner,m_levelManager));
            
            m_stateMachine.AddTransition<Idle,Explore>(m_toExplore);
            
            m_stateMachine.AddTransition<Explore,Collect>(m_toCollect);
            m_stateMachine.AddTransition<Explore,Escape>(m_escape);
            m_stateMachine.AddTransition<Explore,Chase>(m_chase);

            m_stateMachine.AddTransition<Collect,Explore>(m_toExplore);
            m_stateMachine.AddTransition<Collect,Escape>(m_escape);
            m_stateMachine.AddTransition<Collect,Chase>(m_chase);

            m_stateMachine.AddTransition<Chase,Explore>(m_exitFromChase);
            m_stateMachine.AddTransition<Chase,Escape>(m_escape);
            m_stateMachine.AddTransition<Escape,Explore>(m_exitFromEscape);

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
#if UNITY_EDITOR
            Handles.Label(transform.position,m_stateMachine.GetLeafState().GetType().Name);
#endif
       
        }

        protected override void Update()
        {
            m_tickDispatcher.SetDelta(Time.deltaTime);
            m_stateMachine.Update();
            m_stateMachine.Dispatch(m_tickDispatcher);
        }
    }
}