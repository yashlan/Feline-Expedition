using Cinemachine;
using System;
using System.Collections;
using UnityEngine;

public class CameraEffect : Singleton<CameraEffect>
{
    [Header("Camera Shake")]
    [SerializeField]
    private float _shakePower;

    private static float ShakePower => Instance._shakePower;

    static CinemachineBasicMultiChannelPerlin cinemachineBasic;
    static CinemachineVirtualCamera virtualCamera;

    void Start()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();

        cinemachineBasic = 
            virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    #region CHINEMACHINE CAMERA SHAKE
    /// <summary>
    /// defualt shake power = 10, add float value to make dynamic value. example : PlayShakeEffect(100);
    /// </summary>
    public static void PlayShakeEffect()
    {
        Instance.StartCoroutine(PlayShake(ShakePower));
    }

    public static void PlayShakeEffect(float power)
    {
        Instance.StartCoroutine(PlayShake(power));
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
    public static void PlayZoomIn(float targetSize, Action<bool> OnZoomIn = null)
    {
        Instance.StartCoroutine(IPlayZoomIn(targetSize, OnZoomIn));
    }

    public static void PlayZoomOut(float targetSize, Action<bool> OnZoomOut = null)
    {
        Instance.StartCoroutine(IPlayZoomOut(targetSize, OnZoomOut));
    }

    private static IEnumerator IPlayZoomIn(float targetSize, Action<bool> OnZoomIn)
    {
        while (virtualCamera.m_Lens.OrthographicSize > targetSize)
        {
            virtualCamera.m_Lens.OrthographicSize -= 15 * Time.deltaTime;
            yield return null;
        }

        if(virtualCamera.m_Lens.OrthographicSize <= targetSize)
        {
            virtualCamera.m_Lens.OrthographicSize = targetSize;
            OnZoomIn(true);
            yield break;
        }
    }

    private static IEnumerator IPlayZoomOut(float targetSize, Action<bool> OnZoomOut)
    {
        while (virtualCamera.m_Lens.OrthographicSize < targetSize)
        {
            virtualCamera.m_Lens.OrthographicSize += 15 * Time.deltaTime;
            yield return null;
        }

        if (virtualCamera.m_Lens.OrthographicSize >= targetSize)
        {
            virtualCamera.m_Lens.OrthographicSize = targetSize;
            OnZoomOut(true);
            yield break;
        }
    }
    #endregion
}
