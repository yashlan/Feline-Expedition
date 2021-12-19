using UnityEngine;
using UnityEngine.UI;

public enum SliderVolumeType
{
    BGM,
    SFX
}

public class SliderVolumeUI : MonoBehaviour
{
    public SliderVolumeType VolumeType;

    Slider slider;

    AudioManager manager => AudioManager.Instance;

    void Awake()
    {
        slider = GetComponent<Slider>();

        if (VolumeType == SliderVolumeType.BGM)
            slider.value = manager.volume_BGM;
        else
            slider.value = manager.volume_SFX;
    }

    #region ON VALUE CHANGED LISTENER

    public void SetVolumeBGM(float sliderValue)
    {
        manager.AudioMixer.SetFloat(AudioManager.Exspose_BGM, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(PlayerPrefsKey.VOLUME_BGM, sliderValue);
        manager.volume_BGM = PlayerPrefs.GetFloat(PlayerPrefsKey.VOLUME_BGM);
    }

    public void SetVolumeSFX(float sliderValue)
    {
        manager.AudioMixer.SetFloat(AudioManager.Exspose_SFX, Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat(PlayerPrefsKey.VOLUME_SFX, sliderValue);
        manager.volume_SFX = PlayerPrefs.GetFloat(PlayerPrefsKey.VOLUME_SFX);
    }
    #endregion
}
