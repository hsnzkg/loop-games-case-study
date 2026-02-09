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

        private Dictionary<SoundType, AudioClip> m_clipMap;
        private AudioSourcePool m_pool;
        private EventBind<EPlaySound> m_playSoundBind;
        private AudioSource m_currentSource;
        private void Awake()
        {
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
            if (m_currentSource != null && m_currentSource.isPlaying)
            {
                m_currentSource.Stop();
                m_pool.Release(m_currentSource);
                m_currentSource = null;
            }

            AudioSource source = m_pool.Get();

            source.clip = clip;
            source.volume = e.Volume;
            source.loop = false;

            if (e.RandomPitch)
            {
                source.pitch = Random.Range(e.PitchMin, e.PitchMax);
            }
            else
            {
                source.pitch = 1f;
            }

            source.Play();

            m_currentSource = source;

            StartCoroutine(ReleaseWhenFinished(source));
        }
        
        private IEnumerator ReleaseWhenFinished(AudioSource source)
        {
            yield return new WaitWhile(() => source.isPlaying);

            if (m_currentSource == source)
                m_currentSource = null;

            m_pool.Release(source);
        }
    }
}