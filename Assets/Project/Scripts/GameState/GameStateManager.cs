using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.FiniteStateMachine.Runtime.RuntimeMode;
using Project.Scripts.GameState.States;

namespace Project.Scripts.GameState
{
    public static class GameStateManager
    {
        private static StateMachine s_fsm;
        private static Preload s_preloadState;
        private static Gameplay s_gameplayState;
        
        public static void Initialize()
        {
            s_fsm = new StateMachine(new ManualMode());
            s_fsm.AddState<Preload>();
            s_fsm.AddState<Gameplay>();
            s_fsm.AddState<Quit>();
            s_fsm.ChangeState<Preload>();
        }

        public static void RequestStateChange<T>()
        {
            s_fsm.ChangeState<T>();
        }
    }
}