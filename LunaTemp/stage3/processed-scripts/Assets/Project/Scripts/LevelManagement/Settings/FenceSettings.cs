using UnityEngine;

namespace Project.Scripts.LevelManagement.Settings
{
    [CreateAssetMenu(fileName = "FenceData", menuName = "Project/FenceData", order = 0)]
    public class FenceSettings : ScriptableObject
    {
        public GameObject FencePostPrefab;           
        public GameObject HorizontalConnectionPrefab;
        public GameObject VerticalConnectionPrefab;
    }
}