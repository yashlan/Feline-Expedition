using UnityEngine;
using UnityEngine.UI;

public class PlayButtonSoundUI : MonoBehaviour
{
    Button button;
    void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.
            AddListener(() => { AudioManager.PlaySfx(AudioManager.Instance.ButtonEnterClip); });
    }
}
