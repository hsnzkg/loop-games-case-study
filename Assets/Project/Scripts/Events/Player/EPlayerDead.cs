using Project.Scripts.Entity.Player;
using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.Events.Player
{
    public struct EPlayerDead : IEvent
    {
        public readonly PlayerEntity Player;

        public EPlayerDead(PlayerEntity playerEntity)
        {
            Player = playerEntity;
        }
    }
}