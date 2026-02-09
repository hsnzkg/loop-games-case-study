using UnityEngine;

namespace Project.Scripts.Entity.Player.UI.Flag
{
    public class PlayerFlagProvider : MonoBehaviour, IFlagProvider
    {
        [SerializeField] private Sprite m_playerFlag;

        public Sprite GetFlag()
        {
            return m_playerFlag;
        }
    }
}
