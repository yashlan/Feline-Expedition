using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSelectButton : MonoBehaviour
{
    void Awake()
    {
        Select();
    }

    void Select()
    {
        GetComponent<Button>().Select();
        print(gameObject.name + "was selected");
    }
}
