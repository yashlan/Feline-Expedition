using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetNameKeyboard : MonoBehaviour
{
    private Event _keyEvent;
    private KeyCode _newKey;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnGUI()
    {
        _keyEvent = Event.current;

        if (_keyEvent.isKey && _keyEvent.keyCode != KeyCode.Escape)
        {
            GetComponent<Text>().text = $"Nama Tombol : {_keyEvent.keyCode}";
        }
    }
}
