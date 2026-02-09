using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.Sound
{
    public struct EPlaySound : IEvent
    {
        public readonly SoundType Type;
        public readonly float Volume;
        public bool RandomPitch;
        public float PitchMin;
        public float PitchMax;

        public EPlaySound(
            SoundType type,
            float volume = 1f,
            bool randomPitch = false,
            float pitchMin = 0.95f,
            float pitchMax = 1.05f)
        {
            Type = type;
            Volume = volume;
            RandomPitch = randomPitch;
            PitchMin = pitchMin;
            PitchMax = pitchMax;
        }
    }
}