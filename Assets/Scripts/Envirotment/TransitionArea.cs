using DigitalRuby.RainMaker;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionArea : MonoBehaviour
{
    public string Destination;
    public string CheckPointDestination;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.ChangeGameState(GameState.HitTransitionArea);
            AudioManager.StopSFX();
            if (BaseRainScript.Instance != null) BaseRainScript.PauseRainSFX();
            PanelSlideUIController.Instance.FadeIn(
                () => Invoke(nameof(GotoNextDestination), 2f), false);
        }
    }

    private void GotoNextDestination()
    {
        PlayerData.Save(PlayerPrefsKey.HEALTHPOINT, PlayerController.Instance.HealthPoint);
        PlayerData.Save(PlayerPrefsKey.MANAPOINT,   PlayerController.Instance.ManaPoint);
        PlayerData.Save(PlayerPrefsKey.COIN,        PlayerController.Instance.Coins);
        PlayerData.Save(PlayerPrefsKey.LAST_SCENE, Destination);
        PlayerData.Save(PlayerPrefsKey.LAST_CHECKPOINT, CheckPointDestination);
        Invoke(nameof(ChangeScene), 0.25f);
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(Destination);
    }
}
