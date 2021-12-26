using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum NPCType
{
    Rocca,
    Gerrin,
    Gwynn
}

public class DialogueManager : MonoBehaviour
{
    [Header("NPC type")]
    public NPCType nPCType;

    [Header("Component")]
    public GameObject canvasInfo;
    public GameObject canvasDialog;
    public GameObject next;
    public Text textMsg;

    [Header("Attribute")]
    public bool canTalking;
    public int index;

    [Header("To do")]
    [Tooltip("to do when stop conversation")]
    public GameObject todoObject;

    [HideInInspector]
    public AudioClip npcClip;

    float typeSpeed = 0.05f;

    string[] dialogueMessages;

    void Awake()
    {
        canvasDialog.SetActive(false);
        canvasInfo.SetActive(false);
        next.SetActive(false);
        index = -1;

        SetupDialogueMessage();

        if (nPCType == NPCType.Gerrin) npcClip = AudioManager.Instance.NPCGerrinClip;
        if (nPCType == NPCType.Gwynn)  npcClip = AudioManager.Instance.NPCGwynnClip;
        if (nPCType == NPCType.Rocca)  npcClip = AudioManager.Instance.NPCRoccaClip;
    }

    private int GerrinTalkSession() => PlayerData.NpcGerrinTalkSession;
    private int GwynnTalkSession() => PlayerData.NpcGwynnTalkSession;
    private int RoccaTalkSession() => PlayerData.NpcRoccaTalkSession;

    private void SetupDialogueMessage()
    {
        dialogueMessages = null;

        var listDialogueMessage = new List<string>();

        if(nPCType == NPCType.Gerrin)
        {
            if (GerrinTalkSession() == 1)
                listDialogueMessage.Add(NPCDialogSession_1.GERRIN_DIALOGUE_1_0);

            if (GerrinTalkSession() == 2)
            {
                listDialogueMessage.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_0);
                listDialogueMessage.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_1);
                listDialogueMessage.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_2);
                listDialogueMessage.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_3);
                listDialogueMessage.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_4);
                listDialogueMessage.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_5);
            }
        }

        if (nPCType == NPCType.Gwynn)
        {
            if (GwynnTalkSession() == 1)
                listDialogueMessage.Add(NPCDialogSession_1.GWYNN_DIALOGUE_1_0);

            if (GwynnTalkSession() == 2)
            {
                listDialogueMessage.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_0);
                listDialogueMessage.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_1);
                listDialogueMessage.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_2);
                listDialogueMessage.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_3);
                listDialogueMessage.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_4);
                listDialogueMessage.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_5);
            }
        }

        if (nPCType == NPCType.Rocca)
        {
            if (RoccaTalkSession() == 1)
            {
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_0);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_1);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_2);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_3);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_4);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_5);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_6);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_7);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_8);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_9);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_10);
                listDialogueMessage.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_11);
            }

            if (RoccaTalkSession() == 2)
            {
                listDialogueMessage.Add(NPCDialogSession_2.ROCCA_DIALOGUE_2_0);
                listDialogueMessage.Add(NPCDialogSession_2.ROCCA_DIALOGUE_2_1);
            }

            if (RoccaTalkSession() == 3)
            {
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_0);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_1);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_2);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_5);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_6);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_7);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_8);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_9);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_10);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_11);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_12);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_13);
                listDialogueMessage.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_14);
            }

            if (RoccaTalkSession() == 4)
            {
                listDialogueMessage.Add(NPCDialogSession_4.ROCCA_DIALOGUE_4_0);
                listDialogueMessage.Add(NPCDialogSession_4.ROCCA_DIALOGUE_4_1);
            }

            if (RoccaTalkSession() == 5)
            {
                listDialogueMessage.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_0);
                listDialogueMessage.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_1);
                listDialogueMessage.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_2);
                listDialogueMessage.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_3);
                listDialogueMessage.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_4);
            }
        }

        dialogueMessages = listDialogueMessage.ToArray();
    }

    public void StartConversation()
    {
        textMsg.text = null;

        index++;
        if (index > dialogueMessages.Length - 1)
        {
            StopConversation();
            return;
        }

        canvasInfo.SetActive(false);

        PlayerController.Instance.IsTalking = true;
        PlayerController.FreezePosition();

        CameraEffect.PlayZoomIn(18, (x) =>
        {
            canvasDialog.SetActive(true);
            StartCoroutine(StartTyping(dialogueMessages[index]));
        });

    }

    public void NextDialogue()
    {
        textMsg.text = null;

        index++;
        if (index > dialogueMessages.Length - 1)
        {
            StopConversation();
            return;
        }
        StartCoroutine(StartTyping(dialogueMessages[index])); 
    }

    private void StopConversation()
    {
        textMsg.text = null;

        index = -1;
        canTalking = false;
        canvasDialog.SetActive(false);
        canvasInfo.SetActive(false);
        next.SetActive(false);

        PlayerController.Instance.IsTalking = false;

        CameraEffect.PlayZoomOut(25, (x) =>
        {
            PlayerController.UnFreezePosition();

            if (nPCType == NPCType.Gerrin)
            {
                if(RoccaTalkSession() >= 4)
                    SetNewCurrentSession(2, PlayerPrefsKey.GERRIN_TALK_SESSION);
                else if (GerrinTalkSession() == 2 && GwynnTalkSession() == 2)
                    SetNewCurrentSession(5, PlayerPrefsKey.ROCCA_TALK_SESSION);
                else
                    SetNewCurrentSession(1, PlayerPrefsKey.GERRIN_TALK_SESSION);

                if (index == -1 && !PlayerData.IsMapUnlocked && GerrinTalkSession() == 2)
                {
                    todoObject.SetActive(true);
                    PlayerData.IsMapUnlocked = true;
                    PlayerData.Save(PlayerPrefsKey.OPEN_MAP, 1);
                }
            }

            if (nPCType == NPCType.Gwynn)
            {
                if (RoccaTalkSession() == 2)
                {
                    SetNewCurrentSession(2, PlayerPrefsKey.GWYNN_TALK_SESSION);
                    SetNewCurrentSession(4, PlayerPrefsKey.ROCCA_TALK_SESSION);
                }
            }

            if (nPCType == NPCType.Rocca)
            {
                if(RoccaTalkSession() == 1)
                {
                    SetNewCurrentSession(2, PlayerPrefsKey.ROCCA_TALK_SESSION);
                    SetNewCurrentSession(2, PlayerPrefsKey.GWYNN_TALK_SESSION);
                }

                if (RoccaTalkSession() == 3 && GwynnTalkSession() == 2)
                {
                    SetNewCurrentSession(4, PlayerPrefsKey.ROCCA_TALK_SESSION);
                }
            }          
        });
    }

    private IEnumerator StartTyping(string message)
    {
        next.SetActive(false);

        foreach (var letter in message)
        {
            textMsg.text += letter;
            AudioManager.PlaySfx(AudioManager.Instance.TypingClip);
            yield return new WaitForSeconds(typeSpeed);

            if(textMsg.text.Length == message.Length)
            {
                yield return new WaitForSeconds(0.5f);
                next.SetActive(true);
            }
        }
    }

    private void SetNewCurrentSession(int currentSession, string prefKey)
    {
        PlayerData.Save(prefKey, currentSession);
        SetupDialogueMessage();
    }
}
