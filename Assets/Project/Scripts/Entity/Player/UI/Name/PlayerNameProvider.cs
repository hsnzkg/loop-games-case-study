using UnityEngine;

namespace Project.Scripts.Entity.Player.UI
{
    public class PlayerNameProvider : MonoBehaviour, INameProvider
    {
        [SerializeField] private string m_playerName;
        public string GetName()
        {
            return m_playerName;
        }
    }
}