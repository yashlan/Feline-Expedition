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
    public AudioClip[] BgmClip;

    [Header("SFX UI")]
    public AudioClip ButtonEnterClip;
    public AudioClip TypingClip;

    [Header("SFX Environment Map 1")]
    public AudioClip EnviThunderClip;
    public AudioClip EnviBrigdeClip;
    public AudioClip EnviLeverBrigdeClip;

    [Header("SFX Environment Map 2")]
    public AudioClip Envi2LoremIpsumClip;

    [Header("SFX Environment Map 3")]
    public AudioClip Envi3LoremIpsumClip;

    [Header("SFX Player")]
    public AudioClip PlayerHitEnemyClip;
    public AudioClip PlayerThrowClip;
    public AudioClip PlayerMeleeBasicAttackClip;
    public AudioClip PlayerMeleeSpearAttackClip;
    public AudioClip PlayerDashClip;
    public AudioClip PlayerJumpClip;
    public AudioClip PlayerKnockClip;
    public AudioClip PlayerFireballHitShieldClip;
    public AudioClip PlayerDeadClip;

    [Header("SFX NPC")]
    public AudioClip NPCRoccaClip;
    public AudioClip NPCGerrinClip;
    public AudioClip NPCGwynnClip;

    [Header("SFX Enemy Slime")]
    public AudioClip EnemySlimeAttackMeleeClip;
    public AudioClip EnemySlimeDeadClip;

    [Header("SFX Enemy SwordMan Clip")]
    public AudioClip EnemySwordManAttackMeleeClip;
    public AudioClip EnemySwordManHitPlayerClip;
    public AudioClip EnemySwordManDeadClip;

    [Header("SFX Enemy ShieldMan Clip")]
    public AudioClip EnemyShieldManAttackMeleeClip;
    public AudioClip EnemyShieldManHitPlayerClip;
    public AudioClip EnemyShieldManDeadClip;

    [Header("SFX Mid Boss Battle")]
    public AudioClip MidBossAttackMeleeClip;
    public AudioClip MidBossAttackAirClip;
    public AudioClip MidBossDeadClip;


    void Start()
    {
        Load();
        SetBackgroundMusic(BgmClip[0]);
    }

    void Load()
    {
        volume_BGM = PlayerPrefs.GetFloat(PlayerPrefsKey.VOLUME_BGM, volume_BGM);
        volume_SFX = PlayerPrefs.GetFloat(PlayerPrefsKey.VOLUME_SFX, volume_SFX);

        AudioMixer.SetFloat(Exspose_BGM, Mathf.Log10(volume_BGM) * 20);
        AudioMixer.SetFloat(Exspose_SFX, Mathf.Log10(volume_SFX) * 20);
    }

    public static void PauseBGM()
    {
        Instance.BGMSource.Pause();
    }

    public static void UnPauseBGM()
    {
        Instance.BGMSource.UnPause();
    }

    public static void StopSFX()
    {
        Instance.SFXSource.Stop();
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
}
