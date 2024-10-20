using UnityEngine;

namespace Mamavon.Useful
{
    public abstract class SingletonMonoBehaviour<T> : MonoBehaviour
                    where T : SingletonMonoBehaviour<T>
    {
        protected static T _instance;
        public static T Instance
        {
            get
            {
                CheckInstance();
                return _instance;
            }
        }
        protected static void CheckInstance()
        {
            if (_instance == null)
            {
                var in_scene = FindAnyObjectByType<T>();
                if (in_scene != null)
                {
                    _instance = in_scene;

                    if (_instance.SetDontDestroyOnLoadAttribute)
                        DontDestroyOnLoad(in_scene);

                    return;
                }

                var gameobj = new GameObject(typeof(T).Name);
                _instance = gameobj.AddComponent<T>();

                if (_instance.SetDontDestroyOnLoadAttribute)
                    DontDestroyOnLoad(gameobj);

                _instance.OnCreateInstance();
            }
        }

        protected virtual void OnCreateInstance() { }
        protected virtual bool SetDontDestroyOnLoadAttribute => true;


        protected virtual void OnEnable()
        {
            if (_instance is not null && this != _instance)
            {
                Destroy(this);
            }
        }

        protected const RuntimeInitializeLoadType GenerateInstanceTiming = RuntimeInitializeLoadType.AfterSceneLoad;
    }
}