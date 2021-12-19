using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : MonoBehaviour
{
    public GameObject canvasInfo;
    Text textInfo;

    void Awake()
    {
        canvasInfo.SetActive(false);
        textInfo = canvasInfo.GetComponentInChildren<Text>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            textInfo.text = $"Press {OptionsManager.InteractionKey} To Talk";

            canvasInfo.SetActive(true);

            if (Input.GetKeyDown(OptionsManager.InteractionKey))
            {
                DialogueManager.StartConversation();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canvasInfo.SetActive(false);
    }
}
