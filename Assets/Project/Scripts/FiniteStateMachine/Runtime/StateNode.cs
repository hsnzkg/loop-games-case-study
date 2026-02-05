using System.Collections.Generic;

namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public class StateNode
    {
        private readonly StateBase m_stateBase;
        private readonly HashSet<ITransition> m_transitions;

        public StateBase GetStateBase()
        {
            return m_stateBase;
        }

        public HashSet<ITransition> GetTransitions()
        {
            return m_transitions;
        }

        public StateNode(StateBase stateBase)
        {
            m_stateBase = stateBase;
            m_transitions = new HashSet<ITransition>();
        }

        public void AddTransition(StateBase to, IPredicate condition)
        {
            GetTransitions().Add(new Transition(to, condition));
        }
    }
}