using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private GameState _gameState;

    [SerializeField]
    private GameObject _canvasUI;

    // Start is called before the first frame update
    void Start()
    {
        _canvasUI.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //goto menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}
