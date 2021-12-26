using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCController : DialogueManager
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canTalking = true;
            canvasInfo.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        canTalking = false;
        canvasInfo.SetActive(false);
    }

    void Update()
    {
        if (canTalking && PlayerController.Instance.IsIdle() && !PlayerController.Instance.FacingRight)
        {
            if (Input.GetKeyDown(OptionsManager.InteractionKey) && index == -1)
            {
                StartConversation();
                AudioManager.PlaySfx(npcClip);
            }

            if (Input.GetKeyDown(OptionsManager.InteractionKey) && index > -1 && next.activeSelf)
            {
                NextDialogue();
                AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
            }
        }
    }
}
