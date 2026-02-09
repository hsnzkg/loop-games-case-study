using Project.Scripts.FiniteStateMachine.Runtime;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.States
{
    public class AIStateBase : StateBase, ITickable
    {
        protected readonly AIStateContext Context;

        protected AIStateBase(AIStateContext context)
        {
            Context = context;
        }

        public virtual void Tick(float deltaTime)
        {
        }
    }
}