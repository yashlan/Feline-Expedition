using UnityEngine;

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
        var playerPosX = PlayerController.Instance.transform.position.x;

        if (canTalking && PlayerController.Instance.IsIdle())
        {
            if(playerPosX > transform.position.x && !PlayerController.Instance.FacingRight)
            {
                if (Input.GetKeyDown(OptionsManager.InteractionKey) && index == -1)
                {
                    StartConversation();
                    AudioManager.PlaySfx(npcClip);
                }
            }
            else if(playerPosX < transform.position.x && PlayerController.Instance.FacingRight)
            {
                if (Input.GetKeyDown(OptionsManager.InteractionKey) && index == -1)
                {
                    StartConversation();
                    AudioManager.PlaySfx(npcClip);
                }
            }

            if (Input.GetKeyDown(OptionsManager.InteractionKey) && index > -1 && next.activeSelf)
            {
                NextDialogue();
                AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
            }
        }

        UpdateWarningInfo();
    }

    float interval;
    void FixedUpdate()
    {
        if(Time.time > interval)
        {
            SetupDialogueMessage();
            interval = Time.time + 1f;
        }
    }
}
