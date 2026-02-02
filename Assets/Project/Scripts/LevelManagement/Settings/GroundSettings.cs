using UnityEngine;

namespace Project.Scripts.LevelManagement
{
    [CreateAssetMenu(fileName = "GroundData", menuName = "Project/GroundData", order = 0)]
    public class GroundSettings : ScriptableObject
    {
        public GroundTile[] Tiles;
    }
}