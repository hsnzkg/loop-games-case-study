using Project.Scripts.Camera.Events;
using Project.Scripts.Entity.Player;
using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.Spawning.Spawners
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private PlayerEntity m_playerPrefab;
        private bool m_spawned;

        public void Initialize()
        {
            PlayerEntity instance = Instantiate(m_playerPrefab,Vector3.zero,Quaternion.identity);
            instance.Initialize();
            EventBus<EChangeCameraTarget>.Raise(new EChangeCameraTarget(instance.transform));
        }
    }
}