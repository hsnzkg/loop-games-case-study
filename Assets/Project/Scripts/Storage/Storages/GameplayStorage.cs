using Project.Scripts.Spawning.Spawners;
using Project.Scripts.Storage.Runtime;

namespace Project.Scripts.Storage.Storages
{
    public class GameplayStorage : IStorage
    {
        public WeaponCollectableSpawner WeaponCollectableSpawner;
        public void Dispose()
        {
            WeaponCollectableSpawner = null;
        }
    }
}