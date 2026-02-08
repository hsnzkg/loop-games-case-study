using System;
using System.Collections.Generic;
using System.Reflection;
using Project.Scripts.Storage.Runtime.Internal;
using UnityEngine;

namespace Project.Scripts.Storage.Runtime
{
    public static class StorageCenter
    {
        private static List<Type> s_registeredStorageTypes;
        private static List<Type> s_allStorages;

        public static void Initialize()
        {
            Debug.Log($"[Storage Center] : Initialize");
            s_registeredStorageTypes = new List<Type>();
            s_allStorages = typeof(IStorage).GetAssemblies();
        }

        /// <summary>
        /// Registers a DataStorage type for global management.
        /// </summary>
        public static void RegisterStorageType<T>() where T : IStorage, new()
        {
            Type type = typeof(T);
            if (!s_registeredStorageTypes.Contains(type))
            {
                s_registeredStorageTypes.Add(type);
            }
        }
        
        public static void DisposeAllStorages()
        {
            Debug.Log("[Storage] : " + "Disposing All Storages...");
            foreach (Type type in s_allStorages)
            {
                Type storageType = typeof(Storage<>).MakeGenericType(type);
                FieldInfo instanceProperty = storageType.GetField("s_instance", BindingFlags.Static | BindingFlags.NonPublic);
                object instance = instanceProperty?.GetValue(null);
                if(instance == null) 
                {
                    Debug.LogWarning("[Storage] : " + $"{type.Name} Already Disposed Skipping...");
                }
                else
                {
                    MethodInfo resetMethod = storageType.GetMethod("Dispose", BindingFlags.Static | BindingFlags.Public);
                    if (resetMethod == null)
                    {
                        Debug.LogWarning("[Storage] : " + $"{type.Name} Could Not Found Dispose Method...");
                    }
                    resetMethod?.Invoke(null, null);  
                }
            }
            s_registeredStorageTypes = null;
            s_allStorages = null;
        }
        
        public static void DisposeWithCenter(this Type storageType)
        {
            Type genericType = typeof(Storage<>).MakeGenericType(storageType);
            FieldInfo instanceProperty = genericType.GetField("s_instance", BindingFlags.Static | BindingFlags.NonPublic);
            object instance = instanceProperty?.GetValue(null);
            if(instance == null) 
            {
                Debug.LogWarning("[Storage] : " + $"<{storageType.Name}> Already Disposed Skipping...");
            }
            else
            {
                MethodInfo resetMethod = genericType.GetMethod("Dispose", BindingFlags.Static | BindingFlags.Public);
                if (resetMethod == null)
                {
                    Debug.LogWarning("[Storage] : " + $"<{storageType.Name}> Could Not Found Dispose Method...");
                }
                resetMethod?.Invoke(null, null);  
            }
            s_registeredStorageTypes.Remove(genericType);
        }
    }
}