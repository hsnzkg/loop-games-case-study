namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public struct Transition : ITransition
    {
        private readonly StateBase m_to;
        private readonly IPredicate m_condition;

        public readonly StateBase GetTo()
        {
            return m_to;
        }

        public readonly IPredicate GetCondition()
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