using Project.Scripts.Entity.Player;
using Project.Scripts.Level;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;

namespace Project.Scripts.Storage.Storages
{
    public class GameplayStorage : IStorage
    {
        public PlayerEntity Player;
        public WeaponCollectableSpawner WeaponCollectableSpawner;
        public AISpawner AISpawner;
        public LevelManager LevelManager;
        public WeaponCollectableSpawner TrajectorySpawner;

        public void Dispose()
        {
            Player = null;
            WeaponCollectableSpawner = null;
            AISpawner = null;
            LevelManager = null;
            TrajectorySpawner = null;
        }
    }
}