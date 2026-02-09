using System.Collections.Generic;
using Project.Scripts.Entity.PlayerAI.Movement;
using Project.Scripts.Entity.Weapon;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.States
{
    public class Collect : AIStateBase
    {
        private WeaponCollectableEntity[] m_weaponLookupBuffer;
        private readonly WeaponCollectableSpawner m_weaponCollectableSpawner;
        public Collect(AIStateContext context) : base(context)
        {
            m_weaponCollectableSpawner = Storage<GameplayStorage>.GetInstance().WeaponCollectableSpawner;
        }

        protected override void OnEnter()
        {
            m_weaponLookupBuffer = m_weaponCollectableSpawner.GetActiveWeapons();
            float minDistance = float.MaxValue;
            WeaponCollectableEntity closestWeapon = null;
            
            Vector2 entityPosition = Context.EntityTransform.position.ToVector2XY();
            for (int i = 0; i < m_weaponLookupBuffer.Length; i++)
            {
                if(m_weaponLookupBuffer[i] == null) continue;

                Vector2 weaponPos = m_weaponLookupBuffer[i].transform.position.ToVector2XY();
                float distance = Vector2.Distance(entityPosition, weaponPos);
                
                if (!(distance <= Context.AISettings.VisionThreshold)) continue;
                
                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestWeapon = m_weaponLookupBuffer[i];
                }
            }
            if (closestWeapon)
            {
                ((AIInputProvider)Context.AIInputProvider).SetPositionTarget(closestWeapon.transform.position);
            }
        }
    }
}