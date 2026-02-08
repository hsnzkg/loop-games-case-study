using Project.Scripts.EventBus.Runtime;
using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.Storage.Runtime;

namespace Project.Scripts.GameState.States
{
    public class Quit : StateBase
    {
        protected override void OnEnter()
        {
            EventBusCenter.DisposeAllBuses();
            StorageCenter.DisposeAllStorages();
        }
    }
}