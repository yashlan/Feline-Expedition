using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionArea : MonoBehaviour
{
    public string Destination;
    public string CheckPointDestination;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            GameManager.ChangeGameState(GameState.HitTransitionArea);
            AudioManager.StopSFX();
            AudioManager.PauseBGM();
            PanelSlideUIController.Instance.FadeIn(
                () => Invoke(nameof(GotoNextDestination), 2f), false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.ChangeGameState(GameState.HitTransitionArea);
            AudioManager.StopSFX();
            AudioManager.PauseBGM();
            PanelSlideUIController.Instance.FadeIn(
                () => Invoke(nameof(GotoNextDestination), 2f), false);
        }
    }

    private void GotoNextDestination()
    {
        PlayerData.Save(PlayerPrefsKey.LAST_SCENE, Destination);
        PlayerData.Save(PlayerPrefsKey.LAST_CHECKPOINT, CheckPointDestination);
        SceneManager.LoadScene(Destination);
    }
}
