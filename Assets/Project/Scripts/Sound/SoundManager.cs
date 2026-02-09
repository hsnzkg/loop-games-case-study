using System.Collections;
using System.Collections.Generic;
using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.Sound
{
    public class SoundManager : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private SoundEntry[] m_sounds;

        [Header("Pool")]
        [SerializeField] private int m_initialPoolSize = 10;
        private Dictionary<SoundType, AudioSource> m_activeSources;
        private Dictionary<SoundType, AudioClip> m_clipMap;
        private AudioSourcePool m_pool;
        private EventBind<EPlaySound> m_playSoundBind;
        private AudioSource m_currentSource;
        private void Awake()
        {
            m_activeSources = new Dictionary<SoundType, AudioSource>();
            m_playSoundBind = new EventBind<EPlaySound>(OnPlaySound);
            BuildLookup();
            m_pool = new AudioSourcePool(m_initialPoolSize, transform);
        }

        private void OnEnable()
        {
            EventBus<EPlaySound>.Register(m_playSoundBind);
        }

        private void OnDisable()
        {
            EventBus<EPlaySound>.Unregister(m_playSoundBind);
        }

        private void BuildLookup()
        {
            m_clipMap = new Dictionary<SoundType, AudioClip>(m_sounds.Length);

            foreach (SoundEntry entry in m_sounds)
            {
                if (entry.Clip == null) continue;
                m_clipMap[entry.Type] = entry.Clip;
            }
        }

        private void OnPlaySound(EPlaySound e)
        {
            if (!m_clipMap.TryGetValue(e.Type, out AudioClip clip))
                return;

            Play(clip, e);
        }

        private void Play(AudioClip clip, EPlaySound e)
        {
            // Eğer aynı sound type şu an çalıyorsa → durdur
            if (m_activeSources.TryGetValue(e.Type, out AudioSource activeSource))
            {
                if (activeSource != null && activeSource.isPlaying)
                {
                    activeSource.Stop();
                    m_pool.Release(activeSource);
                }

                m_activeSources.Remove(e.Type);
            }

            AudioSource source = m_pool.Get();

            source.clip = clip;
            source.volume = e.Volume;
            source.loop = false;

            if (e.RandomPitch)
                source.pitch = Random.Range(e.PitchMin, e.PitchMax);
            else
                source.pitch = 1f;

            source.Play();

            m_activeSources[e.Type] = source;

            StartCoroutine(ReleaseWhenFinished(source, e.Type));
        }
        
        private IEnumerator ReleaseWhenFinished(AudioSource source, SoundType type)
        {
            yield return new WaitWhile(() => source.isPlaying);

            if (m_activeSources.TryGetValue(type, out AudioSource current))
            {
                if (current == source)
                    m_activeSources.Remove(type);
            }

            m_pool.Release(source);
        }
    }
}