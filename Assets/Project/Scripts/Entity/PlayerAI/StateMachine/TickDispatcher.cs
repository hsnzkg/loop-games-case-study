using Project.Scripts.FiniteStateMachine.Runtime;

namespace Project.Scripts.Entity.PlayerAI.StateMachine
{
    public class TickDispatcher : IDispatcher
    {
        private float m_delta;

        public void SetDelta(float delta)
        {
            m_delta = delta;
        }
        
        public void Dispatch(StateBase state)
        {
            if (state is ITickable tickable)
            {
                tickable.Tick(m_delta);
            }
        }
    }
}