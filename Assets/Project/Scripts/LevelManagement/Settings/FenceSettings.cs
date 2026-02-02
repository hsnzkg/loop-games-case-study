using UnityEngine;

namespace Project.Scripts.LevelManagement
{
    [CreateAssetMenu(fileName = "FenceData", menuName = "Project/FenceData", order = 0)]
    public class FenceSettings : ScriptableObject
    {
        public GameObject FencePostPrefab;           
        public GameObject HorizontalConnectionPrefab;
        public GameObject VerticalConnectionPrefab;
        public float HorizontalConnectionLength;
        public float VerticalConnectionLength;
    }
}