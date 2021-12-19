using NoodleEater.Caravan.System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeButtonMenuValue : MonoBehaviour
{
    public Button buttonNewGame;
    public Button buttonContinue;
    public Button buttonOptions;
    public Button buttonExit;

    private Color white = Color.white;
    private Color transparent = Color.clear;

    private InputLayerSystem _baseInputLayerSystem;

    private InputLayer _currentLayer = InputLayer.ButtonNewGame;

    void SetupButtonsColor(
        Color _newGame, 
        Color _continue, 
        Color _options, 
        Color _exit)
    {
        var cbNewGame = buttonNewGame.colors;
        cbNewGame.normalColor = _newGame;

        var cbContinue = buttonContinue.colors;
        cbContinue.normalColor = _continue;

        var cbOptions = buttonOptions.colors;
        cbOptions.normalColor = _options;

        var cbExit = buttonExit.colors;
        cbExit.normalColor = _exit;
    }

    void SetupNormalColorNewGame()
    {
        var cbNewGame = buttonNewGame.colors;
        cbNewGame.normalColor = white;
    }

    void Start()
    {
        _baseInputLayerSystem = GetComponent<InputLayerSystem>();

        _baseInputLayerSystem.BindAction(InputLayer.ButtonNewGame, SetupNormalColorNewGame);
        _baseInputLayerSystem.BindAction(InputLayer.ButtonContinue);
        _baseInputLayerSystem.BindAction(InputLayer.ButtonOptions);
        _baseInputLayerSystem.BindAction(InputLayer.ButtonExitGame);

        _baseInputLayerSystem.OnLayerChange += (layer) =>
        {
            print(layer);

            if (layer == InputLayer.ButtonNewGame)
            {
                var text = buttonNewGame.GetComponentInChildren<Text>();
                text.color = Color.red;
                SetupButtonsColor(white, transparent, transparent, transparent);
            }

            if (layer == InputLayer.ButtonContinue)
            {
                SetupButtonsColor(transparent, white, transparent, transparent);
            }

            if (layer == InputLayer.ButtonOptions)
            {
                SetupButtonsColor(transparent, transparent, white, transparent);
            }

            if (layer == InputLayer.ButtonExitGame)
            {
                SetupButtonsColor(transparent, transparent, transparent, white);
            }
        };

        _baseInputLayerSystem.EnableLayer(InputLayer.ButtonNewGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) && 
            _currentLayer == InputLayer.ButtonNewGame)
        {
            _currentLayer = InputLayer.ButtonExitGame;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonExitGame);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) &&
            _currentLayer == InputLayer.ButtonNewGame)
        {
            _currentLayer = InputLayer.ButtonContinue;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonContinue);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) &&
            _currentLayer == InputLayer.ButtonContinue)
        {
            _currentLayer = InputLayer.ButtonNewGame;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonNewGame);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) &&
            _currentLayer == InputLayer.ButtonContinue)
        {
            _currentLayer = InputLayer.ButtonOptions;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonOptions);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) &&
            _currentLayer == InputLayer.ButtonOptions)
        {
            _currentLayer = InputLayer.ButtonContinue;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonContinue);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) &&
            _currentLayer == InputLayer.ButtonOptions)
        {
            _currentLayer = InputLayer.ButtonExitGame;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonExitGame);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) &&
            _currentLayer == InputLayer.ButtonExitGame)
        {
            _currentLayer = InputLayer.ButtonOptions;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonOptions);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) &&
            _currentLayer == InputLayer.ButtonExitGame)
        {
            _currentLayer = InputLayer.ButtonNewGame;
            _baseInputLayerSystem.EnableLayer(InputLayer.ButtonNewGame);
        }
    }
}
