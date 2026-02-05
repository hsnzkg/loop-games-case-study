using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.Entity.Player.Movement;
using Project.Scripts.Spawning;
using UnityEngine;

namespace Project.Scripts.Entity.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        private MovementSystem m_movementSystem;
        private CombatSystem m_combatSystem;

        public void Initialize()
        {
            FetchComponents();
            m_movementSystem.Initialize();
            m_combatSystem.Initialize();
        }

        private void FetchComponents()
        {
            m_movementSystem = GetComponentInChildren<MovementSystem>();
            m_combatSystem = GetComponentInChildren<CombatSystem>();
        }
    }
}