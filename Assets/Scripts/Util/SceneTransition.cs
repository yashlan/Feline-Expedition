using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public void changeSceneOnClick(string name) => SceneManager.LoadScene(name);
}
