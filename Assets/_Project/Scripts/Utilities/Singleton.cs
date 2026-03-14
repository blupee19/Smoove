using UnityEngine;

namespace Smoove.Utilities
{
    /// <summary>
    /// Generic MonoBehaviour singleton that persists across scene loads.
    /// Inherit from this to create a singleton component.
    /// </summary>
    /// <typeparam name="T">The type of the singleton.</typeparam>
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                        _instance = FindFirstObjectByType<T>();

                    return _instance;
                }
            }
        }

        protected virtual void Awake()
        {
            lock (_lock)
            {
                if (_instance != null && _instance != this)
                {
                    Debug.LogWarning($"[Singleton] Duplicate instance of {typeof(T).Name} detected. Destroying this one.");
                    Destroy(gameObject);
                    return;
                }

                _instance = this as T;
                DontDestroyOnLoad(gameObject);
            }
        }

        protected virtual void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }
    }
}
