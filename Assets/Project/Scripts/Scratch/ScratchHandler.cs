using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Scratch;
using Project.ThirdParty.ScratchCard.Scripts;
using UnityEngine;

namespace Project.Scripts.Scratch
{
    public class ScratchHandler : MonoBehaviour
    {
        [SerializeField] private ScratchSettings m_scratchSettings;
        private ScratchCardManager m_scratchCardManager;
        private EventBind<EScratch> m_scratchEventBind;

        private void Awake()
        {
            Initialize();
        }

        private void Initialize()
        {
            m_scratchCardManager = GetComponent<ScratchCardManager>();
            m_scratchEventBind = new EventBind<EScratch>(OnScratch);
            m_scratchCardManager.SetBrushTexture(m_scratchSettings.Brush);
            m_scratchCardManager.SetBrushSize(m_scratchSettings.BrushSize);
            m_scratchCardManager.SetBrushOpacity(m_scratchSettings.Opacity);
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
            m_scratchCardManager.Card.ScratchHole(obj.Position,m_scratchSettings.Pressure);
        }
    }
}