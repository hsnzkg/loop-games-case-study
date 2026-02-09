using UnityEngine;

namespace Project.Scripts.Entity.Player.Attributes
{
    [CreateAssetMenu(fileName = "PlayerAttributeSettings", menuName = "Project/PlayerAttributeSettings", order = 0)]
    public class PlayerAttributeSettings : ScriptableObject
    {
        public float Health;
    }
}