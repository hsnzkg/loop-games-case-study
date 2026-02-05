using UnityEngine;

namespace Project.Scripts.Level.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Project/LevelSettings", order = 0)]
    public class LevelSettings : ScriptableObject
    {
        public int Height;
        public int Width;
        public int BlankSpace;
        public float TileSize = 1f;
        public Vector2 Offset;
        public int Seed;
        public bool RandomSeed = true;
        public GroundSettings GroundSettings;
        public FenceSettings FenceSettings;
    }
}