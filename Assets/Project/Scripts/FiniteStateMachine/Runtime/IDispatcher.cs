namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public interface IDispatcher
    {
        void Dispatch(StateBase state);
    }
}