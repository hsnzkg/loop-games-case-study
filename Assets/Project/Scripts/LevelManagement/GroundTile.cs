using UnityEngine;

namespace Project.Scripts.LevelManagement
{
    [System.Serializable]
    public struct GroundTile
    {
        [Range(0f, 1f)] public float Weight;
        public GameObject TilePrefab;
    }
}