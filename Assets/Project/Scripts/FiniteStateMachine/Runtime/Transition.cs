namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public readonly struct Transition : ITransition
    {
        private readonly StateBase m_to;
        private readonly IPredicate m_condition;

        public StateBase GetTo()
        {
            return m_to;
        }

        public IPredicate GetCondition()
        {
            return m_condition;
        }

        public Transition(StateBase to, IPredicate condition)
        {
            m_to = to;
            m_condition = condition;
        }
    }
}