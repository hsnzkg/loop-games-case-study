using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.FiniteStateMachine.Runtime.RuntimeMode;
using Project.Scripts.GameStateManagement.States;

namespace Project.Scripts.GameStateManagement
{
    public static class GameStateManager
    {
        private static StateMachine s_fsm;
        private static Preload s_preloadState;
        private static InGame s_inGameState;


        public static void Initialize()
        {
            s_fsm = new StateMachine(new ManualMode());
            s_fsm.AddState<Preload>();
            s_fsm.AddState<InGame>();
            s_fsm.ChangeState<Preload>();
        }

        public static void RequestStateChange<T>()
        {
            s_fsm.ChangeState<T>();
        }
    }
}