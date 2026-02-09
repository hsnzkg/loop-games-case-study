using System.Collections.Generic;
using Project.Scripts.Entity.Weapon;
using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.Predicates
{
    public class IdleToExplore : IPredicate
    {
        private readonly List<WeaponCollectableEntity> m_weaponInRangeBuffer;
        private readonly WeaponCollectableSpawner m_weaponCollectableSpawner;
        private readonly AIStateContext m_stateContext;
        private WeaponCollectableEntity[] m_weaponLookupBuffer;
        
        public IdleToExplore(AIStateContext context)
        {
            m_weaponInRangeBuffer = new List<WeaponCollectableEntity>();
            m_stateContext = context;
            m_weaponCollectableSpawner = Storage<GameplayStorage>.GetInstance().WeaponCollectableSpawner;
        }
        
        public bool Evaluate()
        {
            return false;
        }

        private bool IsAnyWeaponInRange()
        {
            GetWeaponsInRange();
            return m_weaponInRangeBuffer.Count <= 0;
        }

        private void GetWeaponsInRange()
        {
            m_weaponLookupBuffer = m_weaponCollectableSpawner.GetActiveWeapons();
            m_weaponInRangeBuffer.Clear();
            
            Vector2 entityPosition = m_stateContext.EntityTransform.position.ToVector2XY();
            for (int i = 0; i < m_weaponLookupBuffer.Length; i++)
            {
                if(m_weaponLookupBuffer[i] == null) continue;

                Vector2 weaponPos = m_weaponLookupBuffer[i].transform.position.ToVector2XY();
                float distance = Vector2.Distance(entityPosition, weaponPos);
                if (distance <= m_stateContext.AISettings.VisionThreshold)
                {
                    m_weaponInRangeBuffer.Add(m_weaponLookupBuffer[i]);
                }
            }
        }

        public List<WeaponCollectableEntity> GetWeaponBuffer()
        {
            return m_weaponInRangeBuffer;
        }
    }
}