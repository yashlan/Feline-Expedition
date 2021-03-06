using DigitalRuby.RainMaker;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Ready,
    Playing,
    Paused,
    HitDeadArea,
    HitTransitionArea,
    CutScene,
    GameOver
}

public enum SceneType
{
    map_1,
    map_2,
    map_3_1,
    map_3_2,
    map_3_3,
    mid_boss,
    save_area_1,
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState _gameState;

    [SerializeField]
    private SceneType sceneType;

    [Header("UI")]
    public GameObject TextGameOver;
    public GameObject MiniMap;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject _panelPause;

    public static GameState GameState { get => Instance._gameState; set => Instance._gameState = value; }
    public static SceneType SceneType { get => Instance.sceneType; set => Instance.sceneType = value; }

    void Start()
    {
        OptionsManager.HideMouseCursor();

        if (sceneType == SceneType.map_1) 
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[1]));

        if(sceneType == SceneType.map_2) 
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[2]));

        if(sceneType == SceneType.map_3_1)
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[3]));

        if (sceneType == SceneType.map_3_2)
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[3]));

        if (sceneType == SceneType.map_3_3)
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[3]));

        if (sceneType == SceneType.mid_boss)
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[4]));

        if (sceneType == SceneType.save_area_1)
            ChangeGameState(GameState.Playing,
                () => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[2]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !PlayerController.Instance.IsShopping)
        {
            PauseGame();
        }

        if(_gameState == GameState.HitTransitionArea)
        {
            PlayerController.FreezePosition();
        }

        if(PlayerData.IsMapUnlocked && 
            _gameState == GameState.Playing &&
            Input.GetKeyDown(OptionsManager.OpenMapKey))
        {
            MiniMap.SetActive(true);
        }
        else if(PlayerData.IsMapUnlocked && 
            _gameState == GameState.Playing && 
            Input.GetKeyUp(OptionsManager.OpenMapKey))
        {
            MiniMap.SetActive(false);
        }


#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
#endif
    }

    public static void ShowGameOverText()
    {
        Instance.TextGameOver.SetActive(true);
    }

    public static void ShowGameOverText(string customText)
    {
        var text = Instance.TextGameOver.GetComponent<Text>();
        text.text = customText;
        Instance.TextGameOver.SetActive(true);
    }

    #region FOR PAUSE MENU

    private void PauseGame()
    {
        ChangeGameState(GameState.Paused);
        _panelPause.SetActive(true);

        OptionsManager.ShowMouseCursor();

        Time.timeScale = 0;

        AudioManager.PauseBGM();
        AudioManager.StopSFX();

        if (BaseRainScript.Instance != null)
            BaseRainScript.PauseRainSFX();
    }

    public void ResumeOnClick()
    {
        ChangeGameState(GameState.Playing);
        _panelPause.SetActive(false);

        OptionsManager.HideMouseCursor();

        Time.timeScale = 1;

        AudioManager.UnPauseBGM();

        if (BaseRainScript.Instance != null)
            BaseRainScript.UnPauseRainSFX();
    }

    public void ExitOnClick()
    {
        PlayerData.Save(PlayerPrefsKey.HEALTHPOINT, PlayerController.Instance.HealthPoint);
        PlayerData.Save(PlayerPrefsKey.MANAPOINT, PlayerController.Instance.ManaPoint);
        PlayerData.Save(PlayerPrefsKey.COIN, PlayerController.Instance.Coins);

        Time.timeScale = 1;
        AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[0]);
        SceneManager.LoadScene("home");
    }

    #endregion

    #region UTILITY

    public static void ChangeGameState(GameState gameState, Action OnChange = null)
    {
        Instance._gameState = gameState;
        OnChange?.Invoke();
    }

    public static void OnCutScene(bool EndWaitCondition)
    {
        Instance.StartCoroutine(IOnCutScene(EndWaitCondition));
    }

    private static IEnumerator IOnCutScene(bool EndWaitCondition)
    {
        yield return new WaitUntil(() => EndWaitCondition);
        ChangeGameState(GameState.Playing);
    }
    #endregion
}
