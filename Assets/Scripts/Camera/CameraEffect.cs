using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraEffect : Singleton<CameraEffect>
{
    [Header("Camera Shake")]
    [SerializeField]
    private float _shakePower;

    [Header("Zoom in/out")]
    [SerializeField]
    private float _ortographicSize; 

    private static float ShakePower => Instance._shakePower;
    private static float OrtographicSize => Instance._ortographicSize;

    static CinemachineBasicMultiChannelPerlin cinemachineBasic;
    static float firstOrtoSize;
    static CinemachineVirtualCamera virtualCamera;

    static PlayerController player => PlayerController.Instance;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineBasic = 
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        firstOrtoSize = virtualCamera.m_Lens.OrthographicSize;
    }

    #region CHINEMACHINE CAMERA SHAKE
    public static void PlayShakeEffect()
    {
        Instance.StartCoroutine(PlayShake(ShakePower));
    }

    private static IEnumerator PlayShake(float power)
    {
        cinemachineBasic.m_AmplitudeGain = power;
        cinemachineBasic.m_FrequencyGain = power;
        yield return new WaitForSeconds(0.25f);
        cinemachineBasic.m_AmplitudeGain = 0f;
        cinemachineBasic.m_FrequencyGain = 0f;
        yield break;
    }
    #endregion

    #region ZOOM IN/OUT CAMERA
    public static void PlayZoomInOutEffect()
    {
        Instance.StartCoroutine(PlayZoomInOut(OrtographicSize));
    }

    private static IEnumerator PlayZoomInOut(float ortoSize)
    {
        virtualCamera.m_Lens.OrthographicSize = ortoSize;
        yield return new WaitUntil(() => PlayerData.IsInvincibleShieldWasUnlocked ? !player.IsDefend : !player.IsCharging);
        virtualCamera.m_Lens.OrthographicSize = firstOrtoSize;
        yield break;
    }
    #endregion
}
