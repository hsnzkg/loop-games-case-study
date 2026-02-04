using Project.Scripts.Entity.Player.Combat;
using Project.Scripts.Entity.Player.Movement;
using UnityEngine;

namespace Project.Scripts.Entity.Player
{
    public class PlayerEntity : MonoBehaviour
    {
        private MovementSystem m_movementSystem;
        private CombatSystem m_combatSystem;

        private void Awake()
        {
            FetchComponents();
        }

        private void FetchComponents()
        {
            m_movementSystem = GetComponentInChildren<MovementSystem>();
            m_combatSystem = GetComponentInChildren<CombatSystem>();
        }
    }
}