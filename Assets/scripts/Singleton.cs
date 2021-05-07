using UnityEngine;

/// <summary>
/// Inherit from this base class to create a singleton.
/// e.g. public class MyClassName : Singleton<MyClassName> {}
/// </summary>
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    // Check to see if we're about to be destroyed.
    static bool shuttingDown = false;
    static readonly object lockObject = new object();
    static T instance;

    /// <summary>
    /// Access singleton instance through this property.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (shuttingDown)
            {
                Debug.LogWarning($"[Singleton] Instance '{typeof(T).Name}' already destroyed. Returning null.");
                return null;
            }

            lock (lockObject)
            {
                if (instance is null)
                {
                    // Search for existing instance.
                    instance = FindObjectOfType<T>();

                    // Create new instance if one doesn't already exist.
                    if (instance is null)
                    {
                        // Need to create a new GameObject to attach the singleton to.
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<T>();
                        singletonObject.name = $"{typeof(T).Name} (Singleton)";

                        // Make instance persistent.
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return instance;
            }
        }
    }

    protected virtual void OnApplicationQuit()
    {
        shuttingDown = true;
    }


    protected virtual void OnDestroy()
    {
        shuttingDown = true;
    }
}