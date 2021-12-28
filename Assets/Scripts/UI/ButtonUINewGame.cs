using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonUINewGame : MonoBehaviour
{
    public GameObject panelNewGame;

    public void NewGameOnClick()
    {
        if (!PlayerPrefs.HasKey(PlayerPrefsKey.LAST_SCENE))
        {
            PlayerData.SetValueOnCreateNewGame();
            SceneManager.LoadScene("map_1");
        }
        else
            panelNewGame.SetActive(true);
    }
}
