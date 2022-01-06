using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitShopUI : MonoBehaviour
{
    public KeyCode key;
    public GameObject ObjToEnable;

    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            gameObject.SetActive(false);
            if (ObjToEnable != null)
                ObjToEnable.SetActive(true);
        }
    }
}
