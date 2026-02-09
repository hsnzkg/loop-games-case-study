using Project.Scripts.Level;
using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;

namespace Project.Scripts.Storage.Storages
{
    public class GameplayStorage : IStorage
    {
        public WeaponCollectableSpawner WeaponCollectableSpawner;
        public AISpawner AISpawner;
        public LevelManager LevelManager;

        public void Dispose()
        {
            WeaponCollectableSpawner = null;
            AISpawner = null;
            LevelManager = null;
        }
    }
}