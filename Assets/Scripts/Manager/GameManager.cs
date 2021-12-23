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
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState _gameState;

    [SerializeField]
    private SceneType sceneType; 

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject _panelPause;

    public static GameState GameState { get => Instance._gameState; set => Instance._gameState = value; }
    public static SceneType SceneType { get => Instance.sceneType; set => Instance.sceneType = value; }

    void Start()
    {
        if(sceneType == SceneType.map_1) ChangeGameState(GameState.Playing,() => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[1]));
        if(sceneType == SceneType.map_2) ChangeGameState(GameState.Playing,() => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[2]));
        if(sceneType == SceneType.map_3_1) ChangeGameState(GameState.Playing,() => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[2]));
        if(sceneType == SceneType.map_3_2) ChangeGameState(GameState.Playing,() => AudioManager.SetBackgroundMusic(AudioManager.Instance.BgmClip[2]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    #region FOR PAUSE MENU

    private void PauseGame()
    {
        ChangeGameState(GameState.Paused);
        _panelPause.SetActive(true);

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

        Time.timeScale = 1;

        AudioManager.UnPauseBGM();

        if (BaseRainScript.Instance != null)
            BaseRainScript.UnPauseRainSFX();
    }

    public void ExitOnClick()
    {
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
