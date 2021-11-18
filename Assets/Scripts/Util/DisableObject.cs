using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    // buat event animasi
    public void DisableGameObject() => gameObject.SetActive(false);
}
