using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    public static T Instance { get; protected set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            throw new System.Exception("An instance of this singleton already exists.");
        }
        else
        {
            Instance = (T)this;
        }
    }
}
