using UnityEngine;

namespace Project.Scripts.Scratch
{
    [CreateAssetMenu(fileName = "ScratchSettings", menuName = "Project/ScratchSettings", order = 0)]
    public class ScratchSettings : ScriptableObject
    {
        [Header("Normal Scratch Settings")]
        public Texture2D Brush;
        public float Opacity;
        public float BrushSize;
    }
}