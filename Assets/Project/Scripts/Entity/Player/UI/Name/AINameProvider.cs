using Project.Scripts.Entity.Player.UI.Name;
using UnityEngine;

namespace Project.Scripts.Entity.Player.UI
{
    public class AINameProvider : MonoBehaviour, INameProvider
    {
        private static readonly string[] s_names =
        {
            "Rex",
            "Nova",
            "Blaze",
            "Echo",
            "Viper",
            "Ghost",
            "Zero",
            "Orion",
            "Titan",
            "Pixel",
            "Shadow",
            "Bolt",
            "Drift",
            "Axel",
            "Nyx"
        };

        public string GetName()
        {
            return s_names[Random.Range(0, s_names.Length)];
        }
    }
}