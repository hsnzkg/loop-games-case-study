using UnityEngine;

namespace Project.Scripts.Storage.Runtime
{
    public class Storage<T> where T : class, IStorage, new()
    {
        // Singleton instance
        private static T s_instance;
        
        // Private constructor to prevent instantiation
        private Storage()
        {
        }

        /// <summary>
        /// Gets or sets the current data instance.
        /// Setting the instance triggers OnChanged only if the new value differs from the current value.
        /// </summary>
        public static T GetInstance()
        {
            if (s_instance != null) return s_instance;
            Debug.LogError("[Storage] : " + $"DataStorage<{typeof(T).Name}> has not been created. Call Create() first");
            return null;
        }

        /// <summary>
        /// Gets or sets the current data instance.
        /// Setting the instance triggers OnChanged only if the new value differs from the current value.
        /// </summary>
        public static void SetInstance(T value)
        {
            if (s_instance == null)
            {
                Debug.LogError("[Storage] : " +
                               $"DataStorage<{typeof(T).Name}>: Instance was not created. Call Create() first");
            }
            else
            {
                if (!Equals(s_instance, value))
                {
                    s_instance = value;
                }
            }
        }

        /// <summary>
        /// Creates or retrieves the singleton instance of the data type.
        /// If an initial data instance is provided, it will be used to initialize the data.
        /// </summary>
        public static void Create()
        {
            if (s_instance == null)
            {
                s_instance = new T();
                StorageCenter.RegisterStorageType<T>();
                Debug.Log("[Storage] : " + $" Creating : Storage<{typeof(T).Name}>");
            }
            else
            {
                Debug.LogError("[Storage] : " + $" Failed To Create : Storage<{typeof(T).Name}> Already Exists !");
            }
        }

        public static void Dispose()
        {
            Debug.Log("[Storage] : " + $"{typeof(T).Name}: Dispose");
            s_instance.Dispose();
            s_instance = null;
        }
        
        public static void DisposeWithCenter()
        {
            typeof(T).DisposeWithCenter();
        }
    }
}