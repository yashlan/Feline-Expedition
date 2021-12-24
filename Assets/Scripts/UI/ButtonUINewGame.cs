using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUINewGame : MonoBehaviour
{
    public GameObject panelNewGame;

    public void NewGameOnClick()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKey.LAST_SCENE))
        {
            PlayerData.Save(PlayerPrefsKey.HEALTHPOINT, PlayerData.DEFAULT_HEALTHPOINT + PlayerData.HealthPointExtra);
            PlayerData.Save(PlayerPrefsKey.MANAPOINT, PlayerData.DEFAULT_MANAPOINT);
            PlayerData.Save(PlayerPrefsKey.COIN, 0);

            PlayerData.Save(PlayerPrefsKey.LAST_SCENE, "map_1");
            print("has key last scene : " + PlayerPrefs.HasKey(PlayerPrefsKey.LAST_SCENE));
            SceneManager.LoadScene("map_1");
        }
        else
            panelNewGame.SetActive(true);
    }
}
