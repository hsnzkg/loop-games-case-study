using Project.Scripts.FiniteStateMachine.Runtime;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.Predicates
{
    public class NotPredicate : IPredicate
    {
        private readonly IPredicate m_inner;

        public NotPredicate(IPredicate inner)
        {
            m_inner = inner;
        }

        public bool Evaluate()
        {
            return !m_inner.Evaluate();
        }
    }
}