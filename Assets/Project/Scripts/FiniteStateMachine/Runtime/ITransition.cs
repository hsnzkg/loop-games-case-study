namespace Project.Scripts.FiniteStateMachine.Runtime
{
    public interface ITransition
    {
        StateBase GetTo();
        IPredicate GetCondition();
    }
}