using UnityEngine;

namespace Project.Scripts.Singleton
{
    public abstract class MonoBehaviourSingleton<T> : MonoBehaviour where T : MonoBehaviourSingleton<T>
    {
        private static T s_instance;
        private static bool s_isInitialized;

        private static string GetLogTag()
        {
            return $"[{typeof(T).Name}]";
        }

        public static bool GetHasInstance()
        {
            return s_instance != null;
        }

        public static T GetInstance()
        {
            if (s_instance != null) return s_instance;
            s_instance = FindObjectOfType<T>();
            if (s_instance != null) return s_instance;
            GameObject go = new GameObject(typeof(T).Name);
            s_instance = go.AddComponent<T>();
            return s_instance;
        }

        public static void Initialize()
        {
            if (s_isInitialized)
            {
                LogWarning("Initialize called but already initialized.");
                return;
            }

            _ = GetInstance();
            s_isInitialized = true;
        }

        protected virtual void Awake()
        {
            if (s_instance == null)
            {
                s_instance = (T)this;
                OnAwake();
            }
            else if (s_instance != this)
            {
                LogWarning("Duplicate instance detected and destroyed.");
                OnDuplicateDestroyed();
                Destroy(gameObject);
            }
        }

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        protected virtual void OnDestroy()
        {
            if (s_instance != this) return;
            s_instance = null;
            s_isInitialized = false;
        }

        protected void MakePersistent()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected virtual void OnAwake()
        {
        }

        protected virtual void OnDuplicateDestroyed()
        {
        }

        protected static void Log(string message)
        {
            Debug.Log($"{GetLogTag()} {message}");
        }

        protected static void LogWarning(string message)
        {
            Debug.LogWarning($"{GetLogTag()} {message}");
        }

        protected static void LogError(string message)
        {
            Debug.LogError($"{GetLogTag()} {message}");
        }
    }
}