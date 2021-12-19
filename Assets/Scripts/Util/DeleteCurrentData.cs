using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteCurrentData : MonoBehaviour
{
    public void DeleteAllDataOnClick()
    {
        PlayerPrefs.DeleteKey(PlayerPrefsKey.LAST_CHECKPOINT);
        PlayerPrefs.DeleteKey(PlayerPrefsKey.LAST_SCENE);
        Invoke(nameof(exit), 1f);
    }

    void exit()
    {
        SceneManager.LoadScene(0);
    }
}
