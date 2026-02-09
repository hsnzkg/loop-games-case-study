using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.Events.Weapon
{
    public struct EWeaponClashCollision : IEvent
    {
        public readonly Collider2D From;
        public readonly Collider2D To;

        public EWeaponClashCollision(Collider2D collider, Collider2D collider2D)
        {
            From = collider;
            To = collider2D;
        }
    }
}