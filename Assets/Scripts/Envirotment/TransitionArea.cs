using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionArea : MonoBehaviour
{
    public string Destination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            AudioManager.StopSFX();
            AudioManager.PauseBGM();
            PlayerData.Save(PlayerPrefsKey.LAST_SCENE, Destination);
            SceneManager.LoadScene(Destination);
        }
    }
}
