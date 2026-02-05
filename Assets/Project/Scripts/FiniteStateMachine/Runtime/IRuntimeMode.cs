namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public interface IRuntimeMode
    {
        void Tick(StateMachine fsm);
    }
}