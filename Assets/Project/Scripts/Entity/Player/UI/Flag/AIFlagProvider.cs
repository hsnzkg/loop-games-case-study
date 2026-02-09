using UnityEngine;

namespace Project.Scripts.Entity.Player.UI.Flag
{
    public class AIFlagProvider : MonoBehaviour, IFlagProvider
    {
        [SerializeField] private Sprite[] m_flagSprites;

        public Sprite GetFlag()
        {
            if (m_flagSprites == null || m_flagSprites.Length == 0) return null;
            
            return m_flagSprites[Random.Range(0, m_flagSprites.Length)];
        }
    }
}
