using DigitalRuby.RainMaker;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum GameState
{
    Playing,
    Paused,
    HitDeadArea,
    HitTransitionArea,
    CutScene,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState _gameState;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject _panelPause;

    public static GameState GameState { get => Instance._gameState; set => Instance._gameState = value; }

    void Start()
    {
        _gameState = GameState.Playing;
    }

    void Update()
    {
        //goto menu sementara
        if (Input.GetKeyDown(KeyCode.Escape) && _gameState == GameState.Playing)
        {
            SceneManager.LoadScene(0);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PauseGame();
        }

        if (Input.GetKeyDown(KeyCode.F5))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    #region FOR PAUSE MENU

    private void PauseGame()
    {
        _gameState = GameState.Paused;
        _panelPause.SetActive(true);

        Time.timeScale = 0;

        AudioManager.PauseBGM();
        AudioManager.StopSFX();

        if (BaseRainScript.Instance != null)
            BaseRainScript.PauseRainSFX();
    }

    public void ResumeOnClick()
    {
        _gameState = GameState.Playing;
        _panelPause.SetActive(false);

        Time.timeScale = 1;

        AudioManager.UnPauseBGM();

        if (BaseRainScript.Instance != null)
            BaseRainScript.UnPauseRainSFX();
    }
    #endregion
}
