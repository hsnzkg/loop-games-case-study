namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public class TransitionDispatcher : IDispatcher
    {
        public void Dispatch(StateBase state)
        {
            if (state.GetIsHierarchical())
            {
                state.GetStateMachine().Update();
            } 
        }
    }
}