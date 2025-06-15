using UnityEngine;

namespace Utils
{
    public class Singleton<T>
    {
        public static T Instance { get; private set; }

        public void SetInstance(T instance)
        {
            if (Instance == null)
            {
                Instance = instance;
            }
            else
            {
                Debug.LogWarning($"An instance of {typeof(T).Name} already exists. Ignoring new instance.");
            }
        }
    }
}