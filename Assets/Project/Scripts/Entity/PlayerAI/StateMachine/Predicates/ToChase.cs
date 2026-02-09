using System.Collections.Generic;
using Project.Scripts.Entity.Player;
using Project.Scripts.FiniteStateMachine.Runtime;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.Predicates
{
    public class ToChase : IPredicate
    {
        private readonly List<PlayerEntity> m_playersInRangeBuffer;
        private PlayerAIEntity[] m_aiLookupBuffer;

        private readonly AISpawner m_aiSpawner;
        private readonly AIStateContext m_stateContext;

        public ToChase(AIStateContext context,AISpawner aiSpawner)
        {
            m_playersInRangeBuffer = new List<PlayerEntity>();
            m_stateContext = context;
            m_aiSpawner = aiSpawner;
        }

        public bool Evaluate()
        {
            GetPlayersInRange();
            if (m_playersInRangeBuffer.Count <= 0) return false;
            bool isFoundWeak = DetectWeakness();
            return isFoundWeak;
        }

        private void GetPlayersInRange()
        {
            m_aiLookupBuffer = m_aiSpawner.GetPlayers();
            m_playersInRangeBuffer.Clear();

            PlayerEntity player = Storage<GameplayStorage>.GetInstance().Player;

            Vector2 entityPosition = m_stateContext.EntityTransform.position.ToVector2XY();
            for (int i = 0; i < m_aiLookupBuffer.Length; i++)
            {
                if (m_aiLookupBuffer[i] == null) continue;
                if (m_aiLookupBuffer[i].GetIsDead()) continue;
                if (m_aiLookupBuffer[i] == m_stateContext.Entity) continue;
                Vector2 aiPos = m_aiLookupBuffer[i].transform.position.ToVector2XY();
                float distance = Vector2.Distance(entityPosition, aiPos);
                if (distance <= m_stateContext.AISettings.ChaseVisionThreshold)
                {
                    m_playersInRangeBuffer.Add(m_aiLookupBuffer[i]);
                }
            }

            float playerDistance = Vector2.Distance(entityPosition, player.transform.position.ToVector2XY());
            if (playerDistance <= m_stateContext.AISettings.ChaseVisionThreshold)
            {
                m_playersInRangeBuffer.Add(player);
            }
        }

        private bool DetectWeakness()
        {
            foreach (PlayerEntity player in m_playersInRangeBuffer)
            {
                int weaponCount = player.GetWeaponCount();
                int entityWeaponCount = m_stateContext.Entity.GetWeaponCount();
                bool weaponFlag = entityWeaponCount < weaponCount;
                bool healthFlag = m_stateContext.Entity.GetPercentHealth() > player.GetPercentHealth();
                if (weaponFlag || healthFlag)
                {
                    return true;
                }
            }
            return false;
        }
    }
}