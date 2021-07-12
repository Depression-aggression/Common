using UnityEngine;
using UnityEngine.SceneManagement;

namespace FD.Utils
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        public bool keepAlive = true;
        
        private static T _instance;
        
        private int _instancesInScene;

        public static T Instance
        {
            get
            {
                if (_instance != null)
                    return _instance;

                return FindObjectOfType<T>();
            }
        }

        protected virtual void Awake()
        {
            _instancesInScene++;

            if (Init(Instance, GetComponentName()))
                _instance = (T)this;
        }

        protected virtual void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        protected virtual void OnDestroy()
        {
            var numComponents = GetComponentsInChildren<Component>().Length;

            if (transform.childCount == 0 && numComponents <= 2)
                Destroy(gameObject);

            _instancesInScene--;

            SceneManager.sceneLoaded -= OnSceneLoaded;

            if (Instance == this)
                _instance = null;
        }

        protected virtual void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_instancesInScene < 2)
            {
                if (!keepAlive)
                    DisposeInternal();
            }
            else
            {
                if (!keepAlive && Instance != this)
                    DisposeInternal();
            }
        }

        protected virtual bool Init(T instance, string detectorName)
        {
            if (instance != null && instance != this && instance.keepAlive)
            {
                DisposeInternal();
                return false;
            }

            DontDestroyOnLoad(transform.parent != null ? transform.root.gameObject : gameObject);

            return true;
        }

        protected virtual void DisposeInternal()
        {
            Destroy(this);
        }

        protected abstract string GetComponentName();
    }
}
