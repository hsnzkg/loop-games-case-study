using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.InputManagement;

namespace Project.Scripts.GameStateManagement.States
{
    public class InGame : StateBase
    {
        private readonly GameManager m_gameManager = new GameManager();

        protected override void OnEnter()
        {
            InputManager.Initialize();
            InputManager.Enable();
            m_gameManager.Initialize();
        }

        protected override void OnExit()
        {   
            InputManager.Disable();
        }
    }
}