using Project.Scripts.EventBus.Examples.Events;
using Project.Scripts.EventBus.Runtime;
using UnityEngine;

namespace Project.Scripts.EventBus.Examples
{
    public class ExamplePublisher : MonoBehaviour
    {
        private void Update()
        {
            EventBus<EExampleEvent>.Raise(new EExampleEvent("This is an example event"));
        }
    }
}