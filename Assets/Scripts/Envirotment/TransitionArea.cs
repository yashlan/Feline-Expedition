using DigitalRuby.RainMaker;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TransitionArea : MonoBehaviour
{
    public string Destination;
    public string CheckPointDestination;

    BoxCollider2D boxTransition;

    [Header("Use Panel Before Transit")]
    public bool usePanelConfirm;
    public GameObject panelConfirm;
    public Button button_yes;
    public Button button_no;

    void ButtonYesListener()
    {
        OptionsManager.HideMouseCursor();
        panelConfirm.SetActive(false);
        GameManager.ChangeGameState(GameState.HitTransitionArea);
        AudioManager.StopSFX();
        PanelSlideUIController.Instance.FadeIn(
            () => Invoke(nameof(GotoNextDestination), 2f), false);
        AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
    }

    void ButtonNoListener()
    {
        OptionsManager.HideMouseCursor();
        PlayerController.UnFreezePosition();
        panelConfirm.SetActive(false);
        AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip);
    }

    void Start()
    {
        if (usePanelConfirm)
        {
            button_yes.onClick.AddListener(ButtonYesListener);
            button_no.onClick.AddListener(ButtonNoListener);
        }

        if (GameManager.SceneType == SceneType.map_2)
        {
            boxTransition = GetComponent<BoxCollider2D>();
            boxTransition.enabled = false;
        }
    }

    void Update()
    {
        if(usePanelConfirm)
            PlayerController.Instance.IsTalking = panelConfirm.activeSelf;

        if (GameManager.SceneType == SceneType.map_2)
        {
            if (PlayerData.NpcGerrinTalkSession == 3 &&
                PlayerData.NpcGwynnTalkSession  == 3 &&
                PlayerData.NpcRoccaTalkSession  == 6)
            {
                boxTransition.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (usePanelConfirm)
        {
            panelConfirm.SetActive(true);
            OptionsManager.ShowMouseCursor();
            return;
        }

        if (collision.gameObject.tag == "Player")
        {
            GameManager.ChangeGameState(GameState.HitTransitionArea);
            AudioManager.StopSFX();
            if (BaseRainScript.Instance != null) BaseRainScript.PauseRainSFX();
            PanelSlideUIController.Instance.FadeIn(
                () => Invoke(nameof(GotoNextDestination), 2f), false);
        }
    }

    private void GotoNextDestination()
    {
        PlayerData.Save(PlayerPrefsKey.HEALTHPOINT, PlayerController.Instance.HealthPoint);
        PlayerData.Save(PlayerPrefsKey.MANAPOINT,   PlayerController.Instance.ManaPoint);
        PlayerData.Save(PlayerPrefsKey.COIN,        PlayerController.Instance.Coins);
        PlayerData.Save(PlayerPrefsKey.LAST_SCENE, Destination);
        PlayerData.Save(PlayerPrefsKey.LAST_CHECKPOINT, CheckPointDestination);
        Invoke(nameof(ChangeScene), 0.25f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(Destination);
    }
}
