using Project.Scripts.Entity.Player;
using Project.Scripts.EventBus.Runtime;
using Project.Scripts.Events.Camera;
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
            EventBus<EChangeCameraTarget>.Raise(new EChangeCameraTarget(instance.transform));
        }
    }
}