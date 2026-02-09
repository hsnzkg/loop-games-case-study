using UnityEngine;

namespace Project.Scripts.Sound
{
    [System.Serializable]
    public struct SoundEntry
    {
        public SoundType Type;
        public AudioClip Clip;
    }
}