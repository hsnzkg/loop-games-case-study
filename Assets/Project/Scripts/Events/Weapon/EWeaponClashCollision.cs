using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.Events.Weapon
{
    public struct EWeaponClashCollision : IEvent
    {
        public Collider From;
        public Collider To;
    }
}