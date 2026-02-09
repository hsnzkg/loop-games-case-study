using Project.Scripts.EventBus.Runtime;

namespace Project.Scripts.Events.Camera
{
    public struct ECameraShake : IEvent
    {
        public float? Amplitude;
        public float? Frequency;
        public float? Duration;

        public ECameraShake(
            float? amplitude = null,
            float? frequency = null,
            float? duration = null)
        {
            Amplitude = amplitude;
            Frequency = frequency;
            Duration = duration;
        }
    }
}