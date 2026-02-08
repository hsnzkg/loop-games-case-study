using System.Collections;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Scratch;
using Project.ThirdParty.ScratchCard.Scripts;
using UnityEngine;


namespace Project.Scripts.Scratch
{
    public class ScratchHandler : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera m_scratchCamera;
        [SerializeField] private ScratchSettings m_scratchSettings;
        [SerializeField] private ScratchCardManager m_scratchCardManager;
        private EventBind<EScratch> m_scratchEventBind;
        
        private bool m_isInitialized = false;

        private void Awake()
        {
            Initialize();
        }
    
        private void Initialize()
        {
            m_scratchEventBind = new EventBind<EScratch>(OnScratch);
            
            m_scratchCardManager.SetBrushTexture(m_scratchSettings.Brush);
            m_scratchCardManager.SetBrushSize(m_scratchSettings.BrushSize);
            m_scratchCardManager.SetBrushOpacity(m_scratchSettings.Opacity);
            StartCoroutine(DelayedInitialize());
        }
        
        private IEnumerator DelayedInitialize()
        {
            yield return null;
            m_isInitialized = true;
        }

        public void OnEnable()
        {
            EventBus<EScratch>.Register(m_scratchEventBind);
        }

        public void OnDisable()
        {
            EventBus<EScratch>.Unregister(m_scratchEventBind);
        }
        
        private void OnScratch(EScratch obj)
        {
            if (!m_isInitialized)
            {
                return;
            }
            Vector3 position = m_scratchCamera.WorldToScreenPoint(obj.Position);  
            
            m_scratchCardManager.Card.GetInput().Scratch(position);
            m_scratchCardManager.Card.GetInput().Scratch();
        }
    }
}