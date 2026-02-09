using DG.Tweening;
using UnityEngine;

namespace Project.Scripts.Spawning.Spawners
{
    [CreateAssetMenu(fileName = "WeaponTrajectorySpawnerSettings", menuName = "Project/WeaponTrajectorySpawnerSettings", order = 0)]
    public class WeaponTrajectorySpawnerSettings : ScriptableObject
    {
        public GameObject Prefab;
        public int MaxWeaponsInWorld;
        public int PoolSize;
        public float TweenDuration;
        public float DistanceMultiplier;
        public float SpinDegrees;
        public Ease PositionEase;
        public Ease RotationEase;
        
        public Vector3 GetObjectDefaultSize()
        {
            return Prefab.transform.lossyScale;
        }
    }
}