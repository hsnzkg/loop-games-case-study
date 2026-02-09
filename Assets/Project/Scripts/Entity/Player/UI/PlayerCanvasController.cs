using Project.Scripts.Entity.Player.UI.Flag;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Scripts.Entity.Player.UI
{
    public class PlayerCanvasController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI m_playerNameText;
        [SerializeField] private Image m_flagImage;
        [SerializeField] private Image m_healthFill;
        
        private INameProvider m_nameProvider;
        private IFlagProvider m_flagProvider;
        
        private void Awake()
        {
            m_nameProvider = GetComponent<INameProvider>();
            m_flagProvider = GetComponent<IFlagProvider>();
            
            if (m_nameProvider != null)
                m_playerNameText.text = m_nameProvider.GetName();
            
            if (m_flagProvider != null && m_flagImage != null)
                m_flagImage.sprite = m_flagProvider.GetFlag();
        }

        public void SetHealth(float health)
        {
            m_healthFill.fillAmount = health;
        }
    }
}