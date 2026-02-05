using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public sealed class StateMachine
    {
        private readonly Dictionary<Type, StateNode> m_nodes = new Dictionary<Type, StateNode>();
        private readonly HashSet<ITransition> m_anyTransitions = new HashSet<ITransition>();
        private readonly TransitionDispatcher m_transitionDispatcher = new TransitionDispatcher();
        private StateNode m_defaultNode;
        private StateNode m_activeNode;
        private StateNode m_previousNode;
        private IRuntimeMode m_runtimeMode;

        #region Constructor

        public StateMachine(IRuntimeMode mode)
        {
            SetRuntimeMode(mode);
        }

        #endregion

        #region Runtime Mode

        internal void SetRuntimeMode(IRuntimeMode mode)
        {
            m_runtimeMode = mode;
        }

        #endregion

        #region Node

        private void CreateNode(StateBase stateBase)
        {
            StateNode node = new StateNode(stateBase);
            Type stateType = stateBase.GetType();
            m_nodes.TryAdd(stateType, node);
        }

        private StateNode GetNode(StateBase state)
        {
            return m_nodes.GetValueOrDefault(state.GetType());
        }

        private StateNode GetNode<T>()
        {
            if (!m_nodes.TryGetValue(typeof(T), out StateNode node))
            {
                Debug.Log($"[FSM] : The state of type {typeof(T)} does not exist");
            }
            return node;
        }

        #endregion

        #region Check & Validate
        
        public void Update()
        {
            if (m_activeNode == null)
            {
                ChangeStateInternal(m_defaultNode.GetStateBase());
            }
            m_runtimeMode.Tick(this);
            Dispatch(m_transitionDispatcher);
        }

        #endregion

        #region State

        public void AddState(StateBase state)
        {
            if (HasState(state))
            {
                Debug.LogWarning($"[FSM] State {state.GetType().Name} same type already exists");
                return;
            }

            CreateNode(state);
        }

        public void AddState<T>(params object[] constructorParams) where T : StateBase
        {
            if (HasState<T>())
            {
                Debug.LogWarning($"[FSM] State {typeof(T)} same type already exists");
                return;
            }
            
            T state = (T)Activator.CreateInstance(typeof(T),constructorParams);
            CreateNode(state);
        }

        public void SetDefaultState<T>()
        {
            m_defaultNode = GetNode<T>();
        }
        
        public void SetDefaultState(StateBase child)
        {
            m_defaultNode = GetNode(child);
        }

        internal void ChangeStateInternal(StateBase state)
        {
            if (!HasState(state))
            {
                Debug.LogError($"[FSM] : The state of type {state.GetType()} could not be found !");
                return;
            }
            StateBase previousState = m_activeNode?.GetStateBase();
            
            if (m_activeNode != null)
            {
                if (state == m_activeNode.GetStateBase())
                {
                    Debug.LogWarning($"[FSM] : Trying to change same state from {m_activeNode?.GetStateBase()} to {state}");
                    return;
                }
                
                if (previousState?.Parent != null && previousState.Parent != state)
                {
                    previousState.Parent.OnChildStateChangedInternal(previousState, state);
                }
                
                previousState?.Parent?.OnChildExitedInternal(previousState);

                previousState?.OnExitInternal();
            }

            m_previousNode = m_activeNode;
            m_activeNode = m_nodes[state.GetType()];
            Debug.Log($"[FSM] : Entered to : {state.GetType().Name} , from : {previousState?.GetType().Name ?? "NULL"}");
            state.OnEnterInternal();
            state.Parent?.OnChildEnteredInternal(state);
        }

        public void ChangeState<T>()
        {
            if (!HasState<T>())
            {
                Debug.LogError($"[FSM] : The state of type {typeof(T)} could not be found !");
                return;
            }
            ChangeStateInternal(GetState<T>());
        }

        private void ChangeStateToDefaultState()
        {
            if (m_defaultNode?.GetStateBase() == null)
            {
                Debug.LogError($"[FSM] : Default state is not valid !");
            }
            else
            { 
                ChangeStateInternal(m_defaultNode.GetStateBase());
            }
        }

        public StateBase GetState<T>()
        {
            return GetNode<T>().GetStateBase();
        }

        public StateBase GetActiveState()
        {
            return m_activeNode.GetStateBase();
        }

        public StateBase GetLeafState()
        {
            if (m_activeNode == null) return null;
            StateBase current = m_activeNode.GetStateBase();

            while (current.GetIsHierarchical())
            {
                StateMachine childFsm = current.GetStateMachine();
                StateBase childActive = childFsm?.GetActiveState();
                if (childActive == null) break;
                current = childActive;
            }

            return current;
        }

        public StateBase GetRootState()
        {
            if (m_activeNode == null) return null;

            StateBase current = m_activeNode.GetStateBase();

            while (current.Parent != null)
            {
                current = current.Parent;
            }

            return current;
        }


        private bool HasState(Type stateType)
        {
            return  m_nodes.ContainsKey(stateType);
        }
        
        private bool HasState<T>()
        {
            return HasState(typeof(T));
        }

        private bool HasState(StateBase state)
        { 
            return HasState(state.GetType());
        }

        #endregion

        #region Transition

        public void AddTransition<TFrom, TTo>(IPredicate condition) where TFrom : StateBase where TTo : StateBase
        {
            if (!HasState<TFrom>() || !HasState<TTo>())
            {
                Debug.LogError($"[FSM] : The state of type {typeof(TFrom)} or {typeof(TTo)} does not exist !");
                return;
            }

            GetNode(GetState<TFrom>()).AddTransition(GetState<TTo>(), condition);
        }

        public void AddAnyTransition<TTo>(IPredicate condition)
        {
            if (!HasState<TTo>())
            {
                Debug.LogError($"[FSM] : The state of type {typeof(TTo)} does not exist !");
                return;
            }
            m_anyTransitions.Add(new Transition(GetNode<TTo>().GetStateBase(), condition));
        }

        internal bool HasValidTransition(out ITransition transition)
        {
            transition = null;
            
            foreach (ITransition t in m_anyTransitions)
            {
                if (t.GetCondition().Evaluate())
                {
                    transition = t;
                    return true;
                }
            }
            
            foreach (ITransition t in m_activeNode.GetTransitions())
            {
                if (t.GetCondition().Evaluate())
                {
                    transition = t;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region Dispatch

        public void Dispatch(IDispatcher visitor)
        {
            if (m_activeNode == null) return;

            DispatchRecursive(m_activeNode.GetStateBase(), visitor);
        }

        private void DispatchRecursive(StateBase state, IDispatcher visitor)
        {
            state.Handle(visitor);

            StateBase child = state.GetStateMachine()?.GetActiveState();
            if (child != null)
            {
                DispatchRecursive(child, visitor);
            }
        }

        #endregion
        
    }
}