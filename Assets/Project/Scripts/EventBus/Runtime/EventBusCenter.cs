using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Project.Scripts.EventBus.Runtime
{
    public static class EventBusCenter
    {
        private static List<Type> _eventTypes;
        private static List<Type> _registeredEventTypes;
        
        public static void Initialize()
        {
            Debug.Log("[Event Bus Center] : Initialize");
            _eventTypes = typeof(IEvent).GetAssemblies();
            _registeredEventTypes = InitializeAllBuses();
        }
        
        private static List<Type> InitializeAllBuses()
        {
            List<Type> eventBindTypes = new List<Type>();

            Type typeDef = typeof(EventBus<>);
            foreach (Type eventType in _eventTypes)
            {
                Type busType = typeDef.MakeGenericType(eventType);
                eventBindTypes.Add(busType);
                Debug.Log("[Event Bus] : " + $"Initialized EventBus<{eventType.Name}>");
            }

            return eventBindTypes;
        }
        
        public static void DisposeAllBuses()
        {
            Debug.Log("[Event Bus] : " + "Clearing all buses...");
            if (_registeredEventTypes == null) return;
            for (int i = 0; i < _registeredEventTypes.Count; i++)
            {
                Type busType = _registeredEventTypes[i];
                MethodInfo disposeMethod = busType.GetMethod("Dispose", BindingFlags.Static | BindingFlags.NonPublic);
                if (disposeMethod != null)
                {
                    disposeMethod.Invoke(null, null);
                }
                else
                {
                    Debug.LogError("[Event Bus] : " + busType + " has no <Dispose> method !");
                }
            }
            _eventTypes = null;
            _registeredEventTypes = null;
        }
        
        public static void DisposeWithCenter(this Type eventType)
        {
            Type busType = typeof(EventBus<>).MakeGenericType(eventType);
            MethodInfo disposeMethod = busType.GetMethod("Dispose", BindingFlags.Static | BindingFlags.NonPublic);
            if (disposeMethod != null)
            {
                disposeMethod.Invoke(null, null);
            }
            else
            {
                Debug.LogError("[Event Bus] : " + busType + " has no <Dispose> method !");
            }
            _registeredEventTypes.Remove(busType);
        }
    }
}