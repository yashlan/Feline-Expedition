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
    public GameObject warningInfo;
    public GameObject canvasInfo;
    public GameObject canvasDialog;
    public GameObject next;
    public Text textMsg;

    [Header("Attribute")]
    public bool canTalking;
    public int index;

    [Header("To do")]
    [Tooltip("to do when stop conversation")]
    public GameObject todoObject; // panel shop

    [HideInInspector]
    public AudioClip npcClip;

    float typeSpeed = 0.05f;

    string[] dialogueMessagesGerrin;
    string[] dialogueMessagesGwynn;
    string[] dialogueMessagesRocca;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        canvasDialog.SetActive(false);
        canvasInfo.SetActive(false);
        next.SetActive(false);
        index = -1;

        SetupDialogueMessage();

        if (nPCType == NPCType.Gerrin) npcClip = AudioManager.Instance.NPCGerrinClip;
        if (nPCType == NPCType.Gwynn) npcClip = AudioManager.Instance.NPCGwynnClip;
        if (nPCType == NPCType.Rocca) npcClip = AudioManager.Instance.NPCRoccaClip;
    }

    public void UpdateWarningInfo()
    {
        if (PlayerController.Instance.IsTalking || canTalking)
        {
            warningInfo.SetActive(false);
            return;
        }

        if (GerrinTalkSession() == 1 && GwynnTalkSession() == 1 && RoccaTalkSession() == 1)
        {
            warningInfo.SetActive(nPCType == NPCType.Rocca);
        }
        else if (GerrinTalkSession() == 1 && GwynnTalkSession() == 2 && RoccaTalkSession() == 2)
        {
            warningInfo.SetActive(nPCType == NPCType.Gwynn);
        }
        else if (GerrinTalkSession() == 1 && GwynnTalkSession() == 3 && RoccaTalkSession() == 3)
        {
            warningInfo.SetActive(nPCType == NPCType.Rocca);
        }
        else if (GerrinTalkSession() == 2 && GwynnTalkSession() == 3 && RoccaTalkSession() == 4)
        {
            warningInfo.SetActive(nPCType == NPCType.Gerrin);
        }
        else if (GerrinTalkSession() == 2 && GwynnTalkSession() == 3 && RoccaTalkSession() == 4)
        {
            warningInfo.SetActive(nPCType == NPCType.Gerrin);
        }
        else if (GerrinTalkSession() == 3 && GwynnTalkSession() == 3 && RoccaTalkSession() == 5)
        {
            warningInfo.SetActive(nPCType == NPCType.Rocca);
        }
        else
            warningInfo.SetActive(false);
    }

    private int GerrinTalkSession() => PlayerData.NpcGerrinTalkSession;
    private int GwynnTalkSession() => PlayerData.NpcGwynnTalkSession;
    private int RoccaTalkSession() => PlayerData.NpcRoccaTalkSession;


    public void SetupDialogueMessage()
    {
        dialogueMessagesGerrin = null;
        dialogueMessagesGwynn = null;
        dialogueMessagesRocca = null;

        var listDialogueMessageGerrin = new List<string>();
        var listDialogueMessageGwynn = new List<string>();
        var listDialogueMessageRocca = new List<string>();
        
        listDialogueMessageGerrin.Clear();
        listDialogueMessageGwynn.Clear();
        listDialogueMessageRocca.Clear();


        #region Gerrin

        if (GerrinTalkSession() == 1)
            listDialogueMessageGerrin.Add(NPCDialogSession_1.GERRIN_DIALOGUE_1_0);

        if (GerrinTalkSession() == 2)
        {
            listDialogueMessageGerrin.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_0);
            listDialogueMessageGerrin.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_1);
            listDialogueMessageGerrin.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_2);
            listDialogueMessageGerrin.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_3);
            listDialogueMessageGerrin.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_4);
            listDialogueMessageGerrin.Add(NPCDialogSession_2.GERRIN_DIALOGUE_2_5);
        }

        if(GerrinTalkSession() == 3)
        {
            listDialogueMessageGerrin.Add(NPCDialogSession_3.GERRIN_DIALOGUE_3_0);
        }

        #endregion / Gerrin


        #region Gwynn

        if (GwynnTalkSession() == 1)
            listDialogueMessageGwynn.Add(NPCDialogSession_1.GWYNN_DIALOGUE_1_0);

        if (GwynnTalkSession() == 2)
        {
            listDialogueMessageGwynn.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_0);
            listDialogueMessageGwynn.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_1);
            listDialogueMessageGwynn.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_2);
            listDialogueMessageGwynn.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_3);
            listDialogueMessageGwynn.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_4);
            listDialogueMessageGwynn.Add(NPCDialogSession_2.GWYNN_DIALOGUE_2_5);
        }

        if(GwynnTalkSession() == 3)
        {
            listDialogueMessageGwynn.Add(NPCDialogSession_3.GWYNN_DIALOGUE_3_0);
            //listDialogueMessageGwynn.Add(NPCDialogSession_3.GWYNN_DIALOGUE_3_1);
        }

        #endregion / Gwynn

        #region Rocca

        if (RoccaTalkSession() == 1)
        {
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_0);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_1);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_2);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_3);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_4);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_5);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_6);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_7);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_8);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_9);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_10);
            listDialogueMessageRocca.Add(NPCDialogSession_1.ROCCA_DIALOGUE_1_11);
        }

        if (RoccaTalkSession() == 2)
        {
            listDialogueMessageRocca.Add(NPCDialogSession_2.ROCCA_DIALOGUE_2_0);
            listDialogueMessageRocca.Add(NPCDialogSession_2.ROCCA_DIALOGUE_2_1);
        }

        if (RoccaTalkSession() == 3)
        {
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_0);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_1);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_2);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_5);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_6);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_7);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_8);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_9);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_10);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_11);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_12);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_13);
            listDialogueMessageRocca.Add(NPCDialogSession_3.ROCCA_DIALOGUE_3_14);
        }

        if (RoccaTalkSession() == 4)
        {
            listDialogueMessageRocca.Add(NPCDialogSession_4.ROCCA_DIALOGUE_4_0);
            listDialogueMessageRocca.Add(NPCDialogSession_4.ROCCA_DIALOGUE_4_1);
        }

        if (RoccaTalkSession() >= 5)
        {
            listDialogueMessageRocca.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_0);
            listDialogueMessageRocca.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_1);
            listDialogueMessageRocca.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_2);
            listDialogueMessageRocca.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_3);
            listDialogueMessageRocca.Add(NPCDialogSession_5.ROCCA_DIALOGUE_5_4);
        }

        #endregion / Rocca

        dialogueMessagesGerrin = listDialogueMessageGerrin.ToArray();
        dialogueMessagesGwynn  = listDialogueMessageGwynn.ToArray();
        dialogueMessagesRocca  = listDialogueMessageRocca.ToArray();
    }

    public void StartConversation()
    {
        var playerPosX = PlayerController.Instance.transform.position.x;
        spriteRenderer.flipX = playerPosX > transform.position.x;

        textMsg.text = null;

        index++;

        if(nPCType == NPCType.Gerrin)
        {
            if (index > dialogueMessagesGerrin.Length - 1)
            {
                StopConversation();
                return;
            }
        }

        if (nPCType == NPCType.Gwynn)
        {
            if (index > dialogueMessagesGwynn.Length - 1)
            {
                StopConversation();
                return;
            }
        }

        if (nPCType == NPCType.Rocca)
        {
            if (index > dialogueMessagesRocca.Length - 1)
            {
                StopConversation();
                return;
            }
        }

        canvasInfo.SetActive(false);

        PlayerController.Instance.IsTalking = true;
        PlayerController.FreezePosition();

        CameraEffect.PlayZoomIn(18, (x) =>
        {
            canvasDialog.SetActive(true);

            if (nPCType == NPCType.Gerrin)
                StartCoroutine(StartTyping(dialogueMessagesGerrin[index]));

            if (nPCType == NPCType.Gwynn)
                StartCoroutine(StartTyping(dialogueMessagesGwynn[index]));

            if (nPCType == NPCType.Rocca)
                StartCoroutine(StartTyping(dialogueMessagesRocca[index]));
        });

    }

    public void NextDialogue()
    {
        textMsg.text = null;

        index++;

        if(nPCType == NPCType.Gerrin)
        {
            if (index > dialogueMessagesGerrin.Length - 1)
            {
                StopConversation();
                return;
            }

            StartCoroutine(StartTyping(dialogueMessagesGerrin[index]));
        }

        if (nPCType == NPCType.Gwynn)
        {
            if (index > dialogueMessagesGwynn.Length - 1)
            {
                StopConversation();
                return;
            }

            StartCoroutine(StartTyping(dialogueMessagesGwynn[index]));
        }

        if (nPCType == NPCType.Rocca)
        {
            if (index > dialogueMessagesRocca.Length - 1)
            {
                StopConversation();
                return;
            }

            StartCoroutine(StartTyping(dialogueMessagesRocca[index]));
        }

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
            spriteRenderer.flipX = true;

            PlayerController.UnFreezePosition();

            if(nPCType == NPCType.Rocca)
            {
                if (RoccaTalkSession() == 1)
                {
                    SetNewCurrentSession(2, PlayerPrefsKey.GWYNN_TALK_SESSION);
                    SetNewCurrentSession(2, PlayerPrefsKey.ROCCA_TALK_SESSION);
                }

                if(RoccaTalkSession() == 3)
                {
                    SetNewCurrentSession(2, PlayerPrefsKey.GERRIN_TALK_SESSION);
                    SetNewCurrentSession(4, PlayerPrefsKey.ROCCA_TALK_SESSION);
                }

                if(RoccaTalkSession() == 5)
                    SetNewCurrentSession(6, PlayerPrefsKey.ROCCA_TALK_SESSION);
            }

            if (nPCType == NPCType.Gwynn)
            {
                if(GwynnTalkSession() == 2)
                {
                    SetNewCurrentSession(3, PlayerPrefsKey.GWYNN_TALK_SESSION);
                    SetNewCurrentSession(3, PlayerPrefsKey.ROCCA_TALK_SESSION);
                }

                if(GwynnTalkSession() >= 2)
                    todoObject.SetActive(true);
            }

            if (nPCType == NPCType.Gerrin)
            {
                if(GerrinTalkSession() == 2)
                {
                    SetNewCurrentSession(3, PlayerPrefsKey.GERRIN_TALK_SESSION);
                    SetNewCurrentSession(5, PlayerPrefsKey.ROCCA_TALK_SESSION);

                    todoObject.SetActive(true);
                    PlayerData.Save(PlayerPrefsKey.UNLOCKED_MAP, true);
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
    }
}
