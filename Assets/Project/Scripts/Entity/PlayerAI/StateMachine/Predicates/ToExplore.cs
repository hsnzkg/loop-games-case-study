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
    public class ToExplore : IPredicate
    {
        private readonly List<WeaponCollectableEntity> m_weaponInRangeBuffer;
        private WeaponCollectableEntity[] m_weaponLookupBuffer;
        private readonly WeaponCollectableSpawner m_weaponCollectableSpawner;
        private readonly AIStateContext m_stateContext;

        public ToExplore(AIStateContext context)
        {
            m_weaponInRangeBuffer = new List<WeaponCollectableEntity>();
            m_stateContext = context;
            m_weaponCollectableSpawner = Storage<GameplayStorage>.GetInstance().WeaponCollectableSpawner;
        }

        public bool Evaluate()
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
                if (m_weaponLookupBuffer[i] == null) continue;
                if (m_weaponLookupBuffer[i].GetIsCollecting()) continue;
                Vector2 weaponPos = m_weaponLookupBuffer[i].transform.position.ToVector2XY();
                float distance = Vector2.Distance(entityPosition, weaponPos);
                if (distance <= m_stateContext.AISettings.CollectableVisionThreshold)
                {
                    m_weaponInRangeBuffer.Add(m_weaponLookupBuffer[i]);
                }
            }
        }
    }
}