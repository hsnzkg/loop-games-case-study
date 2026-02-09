using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Sound
{
    public class AudioSourcePool
    {
        private readonly Queue<AudioSource> m_available = new Queue<AudioSource>();
        private readonly List<AudioSource> m_all = new List<AudioSource>();
        private readonly Transform m_parent;

        public AudioSourcePool(int initialSize, Transform parent)
        {
            m_parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                CreateSource();
            }
        }

        private AudioSource CreateSource()
        {
            GameObject go = new GameObject("PooledAudioSource");
            go.transform.SetParent(m_parent);

            AudioSource source = go.AddComponent<AudioSource>();
            source.playOnAwake = false;

            m_available.Enqueue(source);
            m_all.Add(source);

            return source;
        }

        public AudioSource Get()
        {
            if (m_available.Count == 0)
                return CreateSource();

            return m_available.Dequeue();
        }

        public void Release(AudioSource source)
        {
            m_available.Enqueue(source);
        }
    }
}