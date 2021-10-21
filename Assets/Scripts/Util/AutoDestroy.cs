using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    public float timeToDestroy;
    void Start() => Destroy(gameObject, timeToDestroy);
}
