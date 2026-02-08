using UnityEngine;
using UnityEngine.EventSystems;

namespace Project.ThirdParty.ScratchCard.Scripts.Tools
{
    public class EventSystemHelper : MonoBehaviour
    {
        [SerializeField] private EventSystem eventSystem;
        
        private void Awake()
        {
            Component inputSystemUIModule = eventSystem.GetComponent("InputSystemUIInputModule");
            if (inputSystemUIModule != null)
            {
                Destroy(inputSystemUIModule);
            }
            if (!eventSystem.TryGetComponent<StandaloneInputModule>(out _))
            {
                eventSystem.gameObject.AddComponent<StandaloneInputModule>();
            }
        }

        private void OnValidate()
        {
            if (eventSystem == null)
            {
                TryGetComponent(out eventSystem);
            }
        }
    }
}