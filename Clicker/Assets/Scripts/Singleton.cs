using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T _instance;
    private static bool isQuitting = false; // Bandera para saber si la aplicación se está cerrando

    public static T Instance
    {
        get
        {
            // Si la aplicación se está cerrando, no crees un nuevo objeto, solo devuelve null.
            if (isQuitting)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T)}' already destroyed. Returning null.");
                return null;
            }

            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject(typeof(T).Name);
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    public virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(this.gameObject); // Mantenemos esto para que sobreviva entre escenas
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }
    
    // Este método de Unity se llama automáticamente cuando el usuario cierra la aplicación
    // o detiene el modo de juego en el editor.
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
}