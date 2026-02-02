using Project.Scripts.InputManagement;
using Project.Scripts.LevelManagement;
using Project.Scripts.Singleton;

namespace Project.Scripts
{
    public class GameManager : MonoBehaviourSingleton<GameManager>
    {
        private LevelManager _levelManager;
        
        protected override void OnAwake()
        {
            FetchComponents();
            InputManager.Initialize();
            _levelManager.Generate();
        }

        protected override void OnEnable()
        {
            InputManager.Enable();
        }

        protected override void OnDisable()
        {
            InputManager.Disable();
            _levelManager.ClearLevel();
        }

        private void FetchComponents()
        {
            _levelManager = FindObjectOfType<LevelManager>();
        }
    }
}