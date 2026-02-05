using Project.Scripts.FiniteStateMachine.Runtime.RuntimeMode;
using UnityEngine;

namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public partial class StateBase
    {
        private StateMachine m_stateMachine;

        internal StateMachine GetStateMachine()
        {
            return m_stateMachine;
        }

        private void SetStateMachine(StateMachine value)
        {
            m_stateMachine = value;
        }

        internal StateBase Parent;

        internal bool GetIsHierarchical()
        {
            return GetStateMachine() != null;
        }

        protected StateBase(IRuntimeMode runtimeMode)
        {
            if (runtimeMode == null)
            {
                Debug.LogWarning("[FSM] State Config.RuntimeMode is null creating a default one...");
                SetStateMachine(new StateMachine(new AutoMode()));
            }
            else
            {
                SetStateMachine(new StateMachine(runtimeMode));
            }
        }
        
        internal void ConvertHierarchical()
        {
            SetStateMachine(new StateMachine(new AutoMode()));
        }
        
        internal void OnChildStateChangedInternal(StateBase from, StateBase to)
        {
            OnChildStateChanged(from, to);
        }
        
        internal void OnChildExitedInternal(StateBase state)
        {
            OnChildExited(state);
        }

        internal void OnChildEnteredInternal(StateBase state)
        {
            OnChildEntered(state);
        }

        protected virtual void OnChildEntered(StateBase state)
        {
        }

        protected virtual void OnChildExited(StateBase state)
        {
        }
        
        protected virtual void OnChildStateChanged(StateBase from , StateBase to)
        {
        }
    }
}