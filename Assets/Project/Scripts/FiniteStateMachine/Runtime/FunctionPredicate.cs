using System;

namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public class FunctionPredicate : IPredicate
    {
        private readonly Func<bool> m_func;

        public FunctionPredicate(Func<bool> func)
        {
            m_func = func;
        }
        
        public bool Evaluate()
        {
            return m_func.Invoke();
        }
    }
}