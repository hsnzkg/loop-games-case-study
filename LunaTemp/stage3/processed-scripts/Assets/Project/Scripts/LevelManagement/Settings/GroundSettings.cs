using UnityEngine;

namespace Project.Scripts.LevelManagement.Settings
{
    [CreateAssetMenu(fileName = "GroundData", menuName = "Project/GroundData", order = 0)]
    public class GroundSettings : ScriptableObject
    {
        public GroundTile[] Tiles;
    }
}