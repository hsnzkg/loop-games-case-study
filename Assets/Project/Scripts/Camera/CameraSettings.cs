using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Camera
{
    [CreateAssetMenu(fileName = "CameraSettings", menuName = "Project/CameraSettings", order = 0)]
    public class CameraSettings : ScriptableObject
    {
        [Header("Shake")]
        public float ShakeAmplitude = 1.2f;
        public float ShakeFrequency = 2f;
        public float ShakeDuration = 0.25f;
        public Ease ShakeEase = Ease.OutQuad;
    }
}