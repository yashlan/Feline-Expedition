using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void ChangeSceneOnClick(string name)
    {
        SceneManager.LoadScene(name);
    }

    //BUAT BUTTON CONTINUE
    public void GotoLastSceneOnClick()
    {
        SceneManager.LoadScene(PlayerData.LastScene);
    } 
}
