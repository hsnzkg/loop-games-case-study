using System.Collections.Generic;
using Project.Scripts.Entity.Player;
using Project.Scripts.Entity.PlayerAI.Movement;
using Project.Scripts.Level;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;
using Project.Scripts.Storage.Storages;
using Project.Scripts.Utility;
using UnityEngine;

namespace Project.Scripts.Entity.PlayerAI.StateMachine.States
{
    public class Chase : AIStateBase
    {
        private readonly List<PlayerEntity> m_playersInRangeBuffer;
        private PlayerAIEntity[] m_aiLookupBuffer;
        private readonly AISpawner m_aiSpawner;
        private readonly LevelManager m_levelManager;
        
        public Chase(AIStateContext context,AISpawner aiSpawner,LevelManager levelManager) : base(context)
        {
            m_playersInRangeBuffer = new List<PlayerEntity>();
            m_aiSpawner = aiSpawner;
            m_levelManager = levelManager;
        }

        protected override void OnEnter()
        {
            SetChasePosition();
        }

        public override void Tick(float deltaTime)
        {
            SetChasePosition();
        }

        private void SetChasePosition()
        {
            m_aiLookupBuffer = m_aiSpawner.GetPlayers();
            m_playersInRangeBuffer.Clear();
            
            PlayerEntity player = Storage<GameplayStorage>.GetInstance().Player;

            Vector2 entityPosition = Context.EntityTransform.position.ToVector2XY();
            for (int i = 0; i < m_aiLookupBuffer.Length; i++)
            {
                if (m_aiLookupBuffer[i] == null) continue;
                if (m_aiLookupBuffer[i].GetIsDead()) continue;
                if (m_aiLookupBuffer[i] == Context.Entity) continue;
                Vector2 aiPos = m_aiLookupBuffer[i].transform.position.ToVector2XY();
                float distance = Vector2.Distance(entityPosition, aiPos);
                
                if (distance <= Context.AISettings.ChaseVisionThreshold)
                {
                    m_playersInRangeBuffer.Add(m_aiLookupBuffer[i]);
                }
            }

            float playerDistance = Vector2.Distance(entityPosition, player.transform.position.ToVector2XY());
            if (playerDistance <= Context.AISettings.ChaseVisionThreshold)
            {
                m_playersInRangeBuffer.Add(player);
            }
            
            PlayerEntity closestPlayer = null;
            float closestDistance = float.MaxValue;
            foreach (PlayerEntity p in m_playersInRangeBuffer)
            {
                Vector2 playerPos = p.transform.position.ToVector2XY();
                float distance = Vector2.Distance(entityPosition, playerPos);
                
                if (distance <= Context.AISettings.ChaseVisionThreshold)
                {
                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestPlayer = p;
                    }
                }
            }

            if (closestPlayer != null)
            {
                Vector2 threatDir = (closestPlayer.transform.position.ToVector2XY() - entityPosition).normalized;
                Vector2 escapeDir = threatDir;
                
                float escapeDistance = Context.AISettings.EscapeMoveLenght;
                Vector2 desiredPos = entityPosition + escapeDir * escapeDistance;

                Vector2 finalPos = m_levelManager.ClampInsideArea(desiredPos, 0.5f);
                ((AIInputProvider)Context.AIInputProvider).SetPositionTarget(finalPos); 
            }
        }
    }
}