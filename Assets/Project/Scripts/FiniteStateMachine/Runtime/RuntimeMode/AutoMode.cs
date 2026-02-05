namespace Project.Scripts.FiniteStateMachine.Runtime.RuntimeMode
{
    public sealed class AutoMode : IRuntimeMode
    {
        public void Tick(StateMachine fsm)
        {
            bool hasTransition = fsm.HasValidTransition(out ITransition transition);
            if (hasTransition) fsm.ChangeStateInternal(transition.GetTo());
        }
    }
}