using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class MonoSingleton<T> : MonoBehaviour
    {
        public static T Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = (T)(object)this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Debug.LogWarning($"An instance of {typeof(T).Name} already exists. Destroying duplicate.");
                Destroy(gameObject);
            }
        }
    }
}