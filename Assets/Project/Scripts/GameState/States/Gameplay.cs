using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.Input;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;

namespace Project.Scripts.GameState.States
{
    public class Gameplay : StateBase
    {
        private readonly GameManager m_gameManager = new GameManager();

        protected override void OnEnter()
        {
            Storage<GameplayStorage>.Create();
            InputManager.Initialize();
            InputManager.Enable();
            m_gameManager.Initialize();
        }

        protected override void OnExit()
        {   
            InputManager.Disable();
            Storage<GameplayStorage>.DisposeWithCenter();
        }
    }
}