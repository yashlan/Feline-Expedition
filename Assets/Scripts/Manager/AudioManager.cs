using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : SingletonDontDestroy<AudioManager>
{

    public const string Exspose_BGM = "VolumeBGM";
    public const string Exspose_SFX = "VolumeSFX";

    [Header("Component")]
    public AudioMixer AudioMixer;
    public AudioSource BGMSource;
    public AudioSource SFXSource;

    [Header("Volume Value")]
    [Range(0, 1)]
    public float volume_BGM;
    [Range(0, 1)]
    public float volume_SFX;

    [Header("Background Music")]
    public AudioClip BgmClip;

    [Header("SFX UI")]
    public AudioClip ButtonClickClip;

    [Header("SFX Environment")]
    public AudioClip BrigdeClip;

    [Header("SFX Player")]
    public AudioClip PlayerDeadClip;

    [Header("SFX Enemy")]
    public AudioClip EnemyDeadClip;

    [Header("SFX Boss Battle")]
    public AudioClip BossBattleDeadClip;



    void Start()
    {
        volume_BGM = PlayerPrefs.GetFloat(PlayerPrefsKey.VOLUME_BGM, volume_BGM);
        volume_SFX = PlayerPrefs.GetFloat(PlayerPrefsKey.VOLUME_SFX, volume_SFX);

        SetBackgroundMusic(BgmClip);
    }

    public static void PauseBGM()
    {
        Instance.BGMSource.Pause();
    }

    public static void UnPauseBGM()
    {
        Instance.BGMSource.UnPause();
    }

    public static void PlaySfx(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("audio clip sfx belum di isi");
            return;
        }

        Instance.SFXSource.PlayOneShot(clip);
    }

    public static void SetBackgroundMusic(AudioClip clip)
    {

        if (clip == null)
        {
            Debug.LogWarning("audio clip bgm belum di isi");
            return;
        }

        Instance.BGMSource.Stop();
        Instance.BGMSource.clip = clip;
        Instance.BGMSource.Play();
    }

    #region Button Event Listener

    public void PlayClickSound()
    {
        PlaySfx(ButtonClickClip);
    }
    #endregion
}
