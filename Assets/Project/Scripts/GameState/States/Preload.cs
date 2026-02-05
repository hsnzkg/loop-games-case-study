using Project.Scripts.FiniteStateMachine.Runtime;

namespace Project.Scripts.GameState.States
{
    public class Preload : StateBase
    {
        protected override void OnEnter()
        {
            GameStateManager.RequestStateChange<InGame>();
        }

        protected override void OnExit()
        {
        }
    }
}