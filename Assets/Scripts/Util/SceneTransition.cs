using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    //BUAT BUTTON NEW GAME
    public void ChangeSceneOnClick(string name)
    {
        if(!PlayerPrefs.HasKey(PlayerPrefsKey.LAST_SCENE))
        {
            PlayerData.Save(PlayerPrefsKey.LAST_SCENE, name);
            print("has key last scene : " + PlayerPrefs.HasKey(PlayerPrefsKey.LAST_SCENE));
        }
        else
        {
            //TAMPILIN PANEL : ARE YOU SURE TO CREATE NEW GAME. CURRENT DATA WILL BE LOST?
            //JIKA YA HAPUS DATA BUAT DATA BARU, JIKA TIDAK DISABLE PANELNYA
        }

        SceneManager.LoadScene(name);
    }

    //BUAT BUTTON CONTINUE
    public void GotoLastSceneOnClick() => SceneManager.LoadScene(PlayerData.LastScene);
}
