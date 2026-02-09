using Project.Scripts.Entity.Weapon;
using UnityEngine;

namespace Project.Scripts.Spawning.Spawners
{
    [CreateAssetMenu(fileName = "CollectableSpawnerSettings", menuName = "Project/CollectableSpawnerSettings", order = 0)]
    public class WeaponCollectableSpawnerSettings : ScriptableObject
    {
        [Header("Spawn Settings")] 
        public WeaponCollectableEntity WeaponPrefab;
        public int PoolSize = 10;
        public int MaxWeaponsInWorld = 10;
        public float SpawnInterval = 2f;

        [Header("Level Settings")] 
        public float InnerOffset = 1.5f;
        
        public Vector3 GetObjectDefaultSize()
        {
            return WeaponPrefab.transform.localScale;
        }
    }
}